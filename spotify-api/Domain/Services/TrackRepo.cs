using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.Logic;
using SpotifyApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Services
{
    public class TrackRepo : ITrackRepo
    {
        private readonly DataContext _context;

        public TrackRepo(DataContext context) 
        {
            _context = context;
        }

        public void Add(Track t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public void Delete(Track t)
        {
            _context.Remove(t);
            _context.SaveChanges();
        }

        public Task<List<Track>> GetAllAsync()
        {
            return _context.Tracks
                .Include(p => p.Artists)
                .ToListAsync();
        }

        public PagedList<Track> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var collectionBeforPaging = _context.Tracks.Include(a => a.Artists);

            return PagedList<Track>.Create(collectionBeforPaging, pageNumber, pageSize);
        }

        public Task<Track> GetByIdAsync(int id)
        {
            return _context.Tracks
                .Include(a => a.Artists)
                .FirstOrDefaultAsync(t => t.TrackId == id);
        }

        public async void Update(int id, Track newTrack)
        {
            var track = await _context.Tracks.FirstOrDefaultAsync(t => t.TrackId == id);

            track.Artists = newTrack.Artists;
            track.Name = newTrack.Name;
            track.PreviewUrl = newTrack.PreviewUrl;
            track.Href = newTrack.Href;

            _context.Tracks.Update(track);

            await _context.SaveChangesAsync();
        }
    }
}
