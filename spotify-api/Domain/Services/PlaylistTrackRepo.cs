using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Logic;

namespace SpotifyApi.Domain.Services
{
    public class PlaylistTrackRepo : IPlaylistTrackRepo
    {

        private readonly DataContext _context;

        public PlaylistTrackRepo(DataContext context) 
        {
            _context = context;
        }

        public void Add(PlaylistTrack t)
        {
            _context.PlaylistTracks.Add(t);
            _context.SaveChanges();
        }

        public void Delete(PlaylistTrack t)
        {
            _context.PlaylistTracks.Remove(t);
            _context.SaveChanges();
        }

        public Task<List<PlaylistTrack>> GetAllAsync()
        {
            return _context.PlaylistTracks.ToListAsync();
        }

        public PagedList<PlaylistTrack> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var collectionBeforPaging = _context.PlaylistTracks;

            return PagedList<PlaylistTrack>.Create(collectionBeforPaging, pageNumber, pageSize);
        }

        public Task<PlaylistTrack> GetByIdAsync(int id)
        {
            var track = _context.PlaylistTracks.FirstOrDefaultAsync(tr => tr.PlaylistTrackId == id);

            return track;
        }

        public void Update(int id, PlaylistTrack t)
        {
            var playlistTrack = _context.PlaylistTracks.FirstOrDefault(a => a.PlaylistTrackId == id);

            playlistTrack.Name = t.Name;
            playlistTrack.PlaylistTrackId = t.PlaylistTrackId;
            playlistTrack.Href = t.Href;
            playlistTrack.PreviewUrl = t.PreviewUrl;
            
            _context.PlaylistTracks.Update(playlistTrack);

            _context.SaveChanges();
        }
    }
}
