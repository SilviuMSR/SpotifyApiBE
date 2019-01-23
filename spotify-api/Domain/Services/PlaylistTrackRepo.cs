using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.EntityModels;

namespace SpotifyApi.Domain.Services
{
    public class PlaylistTrackRepo : IPlaylistTrack
    {

        private readonly DataContext _context;

        public PlaylistTrackRepo(DataContext context)
        {
            _context = context;
        }

        public void Add(PlaylistTrack t)
        {
            _context.PlaylistTracks.Add(t);
            _context.SaveChangesAsync();
        }

        public void Delete(PlaylistTrack t)
        {
            throw new NotImplementedException();
        }

        public Task<List<PlaylistTrack>> GetAllAsync()
        {
            return _context.PlaylistTracks.ToListAsync();
        }

        public Task<PlaylistTrack> GetByIdAsync(int id)
        {
            var track = _context.PlaylistTracks.FirstOrDefaultAsync(tr => tr.PlaylistTrackId == id);

            return track;
        }

        public void Update(int id, PlaylistTrack t)
        {
            throw new NotImplementedException();
        }
    }
}
