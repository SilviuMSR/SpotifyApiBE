using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.Dtos.ResourceParameters;
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

        public PagedList<PlaylistArtist> GetAllPaginationAsync(PlaylistArtistResourceParameters resourceParams)
        {
            var collectionBeforePaging = _context.PlaylistArtists
                .Include(t => t.Tracks)
                .AsQueryable();

            //if name filter exists
            if (!string.IsNullOrEmpty(resourceParams.Name))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Name == resourceParams.Name);
            }

            return PagedList<PlaylistArtist>.Create(collectionBeforePaging, resourceParams.PageNumber, resourceParams.PageSize);
        }

        public Task<PlaylistArtist> GetByIdAsync(int id)
        {
            var artist = _context.PlaylistArtists.Include(t => t.Tracks).FirstOrDefaultAsync(art => art.PlaylistArtistId == id);

            return artist;
        }

        public void Update(int id, PlaylistArtist t)
        {
            var playlistArtist = _context.PlaylistArtists.Include(tr => tr.Tracks).FirstOrDefault(art => art.PlaylistArtistId == id);

            playlistArtist.ImgUri = t.ImgUri;
            playlistArtist.Name = t.Name;
            playlistArtist.PlaylistArtistId = t.PlaylistArtistId;
            playlistArtist.Tracks = t.Tracks;
            playlistArtist.Uri = t.Uri;

            _context.PlaylistArtists.Update(playlistArtist);

            _context.SaveChanges();
        }
    }
}
