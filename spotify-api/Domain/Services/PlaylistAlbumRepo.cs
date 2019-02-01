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
    public class PlaylistAlbumRepo : IPlaylistAlbumRepo
    {

        private readonly DataContext _context;

        public PlaylistAlbumRepo(DataContext context)
        {
            _context = context;
        }

        public void Add(PlaylistAlbum t)
        {
            _context.PlaylistAlbums.Add(t);
            _context.SaveChanges();
        }

        public void Delete(PlaylistAlbum t)
        {
            _context.PlaylistAlbums.Remove(t);
            _context.SaveChanges();
        }

        public Task<List<PlaylistAlbum>> GetAllAsync()
        {
            return _context.PlaylistAlbums
                .Include(t => t.Tracks)
                .ToListAsync();
        }

        public PagedList<PlaylistAlbum> GetAllPaginationAsync(PlaylistAlbumResourceParameters resourceParams)
        {
            var collectionBeforePaging = _context.PlaylistAlbums
                .Include(t => t.Tracks)
                .AsQueryable();

            //filter by type if type exists
            if (!string.IsNullOrEmpty(resourceParams.Type))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Type == resourceParams.Type);
            }

            //filter by name if name =||=
            if (!string.IsNullOrEmpty(resourceParams.Name))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Name == resourceParams.Name);
            }

            return PagedList<PlaylistAlbum>.Create(collectionBeforePaging, resourceParams.PageNumber, resourceParams.PageSize);
        }

        public Task<PlaylistAlbum> GetByIdAsync(int id)
        {
            var album = _context.PlaylistAlbums.Include(t => t.Tracks).FirstOrDefaultAsync(a => a.PlaylistAlbumId == id);

            return album;
        }

        public void Update(int id, PlaylistAlbum t)
        {
            var playlistAlbum = _context.PlaylistAlbums
                .Include(track => track.Tracks)
                .FirstOrDefault(a => a.PlaylistAlbumId == id);

            playlistAlbum.ImgUri = t.ImgUri;
            playlistAlbum.Name = t.Name;
            playlistAlbum.PlaylistAlbumId = t.PlaylistAlbumId;
            playlistAlbum.Tracks = t.Tracks;
            playlistAlbum.Type = t.Type;

            _context.PlaylistAlbums.Update(playlistAlbum);

            _context.SaveChanges();

        }
    }
}
