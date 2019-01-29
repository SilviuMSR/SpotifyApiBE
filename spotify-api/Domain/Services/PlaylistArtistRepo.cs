using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Logic;

namespace SpotifyApi.Domain.Services
{
    public class PlaylistArtistRepo : IPlaylistArtist
    {

        private readonly DataContext _context;

        public PlaylistArtistRepo(DataContext context)
        {
            _context = context;
        }

        public void Add(PlaylistArtist t)
        {
            _context.PlaylistArtists.Add(t);
            _context.SaveChanges();
        }

        public void Delete(PlaylistArtist t)
        {
            _context.PlaylistArtists.Remove(t);
            _context.SaveChanges();
        }

        public Task<List<PlaylistArtist>> GetAllAsync()
        {
            return _context.PlaylistArtists
                .Include(t => t.Tracks)
                .ToListAsync();
        }

        public PagedList<PlaylistArtist> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var collectionBeforePaging = _context.PlaylistArtists.Include(t => t.Tracks);

            return PagedList<PlaylistArtist>.Create(collectionBeforePaging, pageNumber, pageSize);
        }

        public Task<PlaylistArtist> GetByIdAsync(int id)
        {
            var artist = _context.PlaylistArtists.Include(t => t.Tracks).FirstOrDefaultAsync(art => art.PlaylistArtistId == id);

            return artist;
        }

        public async void Update(int id, PlaylistArtist t)
        {
            var playlistArtist = await _context.PlaylistArtists.Include(tr => tr.Tracks).FirstOrDefaultAsync(art => art.PlaylistArtistId == id);

            playlistArtist.ImgUri = t.ImgUri;
            playlistArtist.Name = t.Name;
            playlistArtist.PlaylistArtistId = t.PlaylistArtistId;
            playlistArtist.Tracks = t.Tracks;
            playlistArtist.Uri = t.Uri;

            _context.PlaylistArtists.Update(playlistArtist);

            await _context.SaveChangesAsync();
        }
    }
}
