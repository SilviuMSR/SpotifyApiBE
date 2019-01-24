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
using SpotifyApi.Domain.Logic;

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
        private readonly IUrlHelper _urlHelper;


        public TrackController(ITrackRepo trackRepo, 
            IMapper mapper,
            IUrlHelper urlHelper)
        {
            _trackRepo = trackRepo;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "Get Tracks")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {
            //Also must modify paginationAsync name since it is not async
            var tracks = _trackRepo.GetAllPaginationAsync(resourceParameters.PageNumber, resourceParameters.PageSize);

            return Ok(tracks);
        }

        //this part must be refactored and added to another folder


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

            var mappedTrack = _mapper.Map<Track>(track);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrackDto trackDto)
        {

            var mappedTrack = _mapper.Map<Track>(trackDto);

            _trackRepo.Update(id, mappedTrack);

            var newTrack = await _trackRepo.GetByIdAsync(id);

            var updatedTrack = _mapper.Map<TrackDto>(newTrack);

            return Ok(updatedTrack);
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
