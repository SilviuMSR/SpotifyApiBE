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
    public class PlaylistArtistController : ControllerBase
    {

        private readonly IPlaylistArtist _playlistArtistRepo;
        private readonly IMapper _mapper;

        public PlaylistArtistController(IPlaylistArtist playlistArtistRepo, IMapper mapper)
        {
            _playlistArtistRepo = playlistArtistRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var artists = await _playlistArtistRepo.GetAllAsync();

            return Ok(artists);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlaylistArtistDto artistDto)
        {
            var artist = _mapper.Map<PlaylistArtist>(artistDto);
            _playlistArtistRepo.Add(artist);
            return Ok(artist);
        }
    }
}
