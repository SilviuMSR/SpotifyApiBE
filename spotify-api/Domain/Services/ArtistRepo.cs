using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Services
{
    public class ArtistRepo : IArtistRepo 
    {
        private readonly DataContext _context;

        public ArtistRepo(DataContext context) 
        {
            _context = context;
        }


        public void Add(Artist t)
        {
            _context.Artists.Add(t);
            _context.SaveChanges();
        }

        public void Delete(Artist t)
        {
            _context.Artists.Remove(t);
            _context.SaveChanges();
        }

        public Task<List<Artist>> GetAllAsync()
        {
            return _context.Artists.Include(t => t.Track).ToListAsync();
        }

        public Task<Artist> GetByIdAsync(int id)
        {
            var artist = _context.Artists.Include(t => t.Track).FirstOrDefaultAsync(a => a.ArtistId == id);

            return artist;
        }

        public void Update(int id, Artist newArtist)
        {
            var artist = _context.Artists.FirstOrDefault(a => a.ArtistId == id);
            
            artist.ImgUri = artist.ImgUri;
            artist.Name = artist.Name;
            artist.Uri = artist.Uri;

            _context.Artists.Update(artist);

            _context.SaveChanges();

        }
    }
}
