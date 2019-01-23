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
    public class PlaylistTrackController : ControllerBase
    {
        private readonly IPlaylistTrack _playlistTrackRepo;
        private readonly IMapper _mapper;

        public PlaylistTrackController(IPlaylistTrack playlistTrackRepo, IMapper mapper)
        {
            _playlistTrackRepo = playlistTrackRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tracks = await _playlistTrackRepo.GetAllAsync();

            return Ok(tracks);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PlaylistTrackDto trackDto)
        {
            var track = _mapper.Map<PlaylistTrack>(trackDto);
            _playlistTrackRepo.Add(track);
            return Ok(track);
        }
    }
}
