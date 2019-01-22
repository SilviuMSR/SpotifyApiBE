using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Models;
using SpotifyApi.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using SpotifyApi.Domain.Dtos;

namespace SpotifyApi.Controllers
{
    [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackRepo _trackRepo;
        private readonly IMapper _mapper;

        public TrackController(ITrackRepo trackRepo, IMapper mapper)
        {
            _trackRepo = trackRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tracks = await _trackRepo.GetAllAsync();
            var mappedTracks = _mapper.Map<IEnumerable<TrackDto>>(tracks);

            return Ok(mappedTracks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var track = await _trackRepo.GetByIdAsync(id);
        
            if (track == null)
            {
                return NotFound();
            }

            var mappedTrack = _mapper.Map<TrackDto>(track);

            return Ok(mappedTrack);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrackDto trackDto)
        {
            var track = _mapper.Map<Track>(trackDto);

            _trackRepo.Add(track);

            return Created($"http://localhost:5000/api/track/{track.TrackId}", trackDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrackDto trackDto)
        {

            var newTrack = await _trackRepo.GetByIdAsync(id);

            if(newTrack == null)
            {
                return NotFound();
            }

            var mappedTrack = _mapper.Map<Track>(trackDto);

            _trackRepo.Update(id, mappedTrack);

            return Ok(mappedTrack);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var track = await _trackRepo.GetByIdAsync(id);

            if(track == null)
            {
                return NotFound();
            }

            _trackRepo.Delete(track);

            var mappedTrack = _mapper.Map<TrackDto>(track);

            return Ok(mappedTrack);
        }
    }

}
