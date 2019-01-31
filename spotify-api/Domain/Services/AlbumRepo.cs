using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.Models;
using SpotifyApi.Domain.Logic;

namespace SpotifyApi.Domain.Services
{
    public class AlbumRepo : IAlbmRepo
    {
        private readonly DataContext _context;

        public AlbumRepo(DataContext context)
        {
            _context = context;
        }

        public void Add(Album t)
        {
            _context.Albums.Add(t);
            _context.SaveChanges();
        }

        public void Delete(Album t)
        {
            _context.Albums.Remove(t);
            _context.SaveChanges();
        }

        public async Task<Album> GetAlbumByNameAsync(string albumName)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(a => a.Name == albumName);

            await _context.SaveChangesAsync();

            return album;
        }

        public Task<List<Album>> GetAllAsync()
        {
            return _context.Albums
                .Include(t => t.Tracks)
                .ToListAsync();
        }

        public PagedList<Album> GetAllPaginationAsync(int pageNumber, int pageSize)
        {

            var collectionBeforPaging = _context.Albums.Include(t => t.Tracks);

            return PagedList<Album>.Create(collectionBeforPaging, pageNumber, pageSize);

        }

        public Task<Album> GetByIdAsync(int id)
        {
            var album = _context.Albums.Include(t => t.Tracks).FirstOrDefaultAsync(a => a.AlbumId == id);

            return album;
        }

        public async void Update(int id, Album newAlbum)
        {
            var album = _context.Albums.Include(t => t.Tracks).FirstOrDefault(a => a.AlbumId == id);

            album.ImgUri = newAlbum.ImgUri;
            album.Name = newAlbum.Name;
            album.Type = newAlbum.Type;
            album.Tracks = newAlbum.Tracks;

            _context.Albums.Update(album);

            _context.SaveChanges();


        }
    }
}
