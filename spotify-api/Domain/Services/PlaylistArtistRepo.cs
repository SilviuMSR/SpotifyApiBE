using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.EntityModels;

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
            throw new NotImplementedException();
        }

        public Task<List<PlaylistArtist>> GetAllAsync()
        {
            return _context.PlaylistArtists
                .Include(t => t.Tracks)
                .ToListAsync();
        }

        public Task<PlaylistArtist> GetByIdAsync(int id)
        {
            var artist = _context.PlaylistArtists.Include(t => t.Tracks).FirstOrDefaultAsync(art => art.PlaylistArtistId == id);

            return artist;
        }

        public void Update(int id, PlaylistArtist t)
        {
            throw new NotImplementedException();
        }
    }
}
