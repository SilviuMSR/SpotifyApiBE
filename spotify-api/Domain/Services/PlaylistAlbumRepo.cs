using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.EntityModels;

namespace SpotifyApi.Domain.Services
{
    public class PlaylistAlbumRepo : IPlaylistAlbum
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
            throw new NotImplementedException();
        }

        public Task<List<PlaylistAlbum>> GetAllAsync()
        {
            return _context.PlaylistAlbums
                .Include(t => t.Tracks)
                .ToListAsync();
        }

        public Task<PlaylistAlbum> GetByIdAsync(int id)
        {
            var album = _context.PlaylistAlbums.Include(t => t.Tracks).FirstOrDefaultAsync(a => a.PlaylistAlbumId == id);

            return album;
        }

        public void Update(int id, PlaylistAlbum t)
        {
            throw new NotImplementedException();
        }
    }
}
