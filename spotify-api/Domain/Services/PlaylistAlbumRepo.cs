﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Logic;

namespace SpotifyApi.Domain.Services
{
    public class PlaylistAlbumRepo : IPlaylistAlbum
    {

        private readonly DataContext _context;

        public PlaylistAlbumRepo(DataContext context)
        {
            _context = context;
        }

        public async void Add(PlaylistAlbum t)
        {
            _context.PlaylistAlbums.Add(t);
            await _context.SaveChangesAsync();
        }

        public async void Delete(PlaylistAlbum t)
        {
            _context.PlaylistAlbums.Remove(t);
            await _context.SaveChangesAsync();
        }

        public Task<List<PlaylistAlbum>> GetAllAsync()
        {
            return _context.PlaylistAlbums
                .Include(t => t.Tracks)
                .ToListAsync();
        }

        public PagedList<PlaylistAlbum> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var collectionBeforePaging = _context.PlaylistAlbums.Include(t => t.Tracks);
            
            return PagedList<PlaylistAlbum>.Create(collectionBeforePaging, pageNumber, pageSize);
        }

        public Task<PlaylistAlbum> GetByIdAsync(int id)
        {
            var album = _context.PlaylistAlbums.Include(t => t.Tracks).FirstOrDefaultAsync(a => a.PlaylistAlbumId == id);

            return album;
        }

        public async void Update(int id, PlaylistAlbum t)
        {
            var playlistAlbum = await _context.PlaylistAlbums.Include(track => track.Tracks).FirstOrDefaultAsync(a => a.PlaylistAlbumId == id);

            playlistAlbum.ImgUri = t.ImgUri;
            playlistAlbum.Name = t.Name;
            playlistAlbum.PlaylistAlbumId = t.PlaylistAlbumId;
            playlistAlbum.Tracks = t.Tracks;
            playlistAlbum.Type = t.Type;

            _context.PlaylistAlbums.Update(playlistAlbum);

            await _context.SaveChangesAsync();

        }
    }
}