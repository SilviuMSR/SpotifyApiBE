using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Controllers
{

    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistAlbumController : ControllerBase
    {

        private readonly IPlaylistAlbum _playlistAlbumRepo;
        private readonly IMapper _mapper;

        public PlaylistAlbumController(IPlaylistAlbum playlistAlbumRepo, IMapper mapper)
        {
            _playlistAlbumRepo = playlistAlbumRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var albums = await _playlistAlbumRepo.GetAllAsync();

            return Ok(albums);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlaylistAlbumDto albumDto)
        {
            var album = _mapper.Map<PlaylistAlbum>(albumDto);
            _playlistAlbumRepo.Add(album);
            return Ok(album);
        }


    }
}
