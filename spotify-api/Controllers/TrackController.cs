using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Models;
using SpotifyApi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using SpotifyApi.Domain.Dtos;
using SpotifyApi.Domain.Logic.Links;
using System.Collections.Generic;
using System.Linq;

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
        private readonly ILinkService<TrackDto> _linkService;


        public TrackController(ITrackRepo trackRepo, 
            IMapper mapper,
            ILinkService<TrackDto> linkService)
        {
            _trackRepo = trackRepo;
            _mapper = mapper;

            _linkService = linkService;
        }

        [HttpGet(Name = "GetTracks")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {
            //Also must modify paginationAsync name since it is not async
            var tracks = _trackRepo.GetAllPaginationAsync(resourceParameters.PageNumber,
                resourceParameters.PageSize);

            var mappedTracks = _mapper.Map<IEnumerable<TrackDto>>(tracks);

            //constructing links to previous and next pages
            var previousPage = tracks.HasPrevious ?
                _linkService.CreateResourceUri(resourceParameters, ResourceType.PreviousPage) : null;

            var nextPage = tracks.HasNext ?
                _linkService.CreateResourceUri(resourceParameters, ResourceType.NextPage) : null;


            mappedTracks = mappedTracks.Select(track =>
            {
                track = _linkService.CreateLinks(track);
                return track;
            });

            var paginationMetadata = new
            {
                totalCount = tracks.TotalCount,
                pageSize = tracks.PageSize,
                currentPage = tracks.CurrentPage,
                totalPages = tracks.TotalPages,
                previousPageLink = previousPage,
                nextPageLink = nextPage
            };

            return Ok(new
            {
                Values = mappedTracks,
                Links = paginationMetadata
            });
        }

        [HttpGet("{id}", Name = "GetTrackById")]
        public async Task<IActionResult> Get(int id)
        {
            var track = await _trackRepo.GetByIdAsync(id);
        
            if (track == null)
            {
                return NotFound();
            }

            var mappedTrack = _mapper.Map<TrackDto>(track);

            return Ok(_linkService.CreateLinks(mappedTrack));
        }

        [HttpPost(Name = "CreateTrack")]
        public async Task<IActionResult> Post([FromBody] TrackDto trackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var track = _mapper.Map<Track>(trackDto);

            _trackRepo.Add(track);

            var mappedTrack = _mapper.Map<TrackDto>(track);

            return Ok(_linkService.CreateLinks(mappedTrack));
        }

        [HttpPut("{id}", Name = "UpdateTrack")]
        public async Task<IActionResult> Update(int id, [FromBody] TrackDto trackDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var mappedTrack = _mapper.Map<Track>(trackDto);

            _trackRepo.Update(id, mappedTrack);

            var newTrack = await _trackRepo.GetByIdAsync(id);

            var updatedTrack = _mapper.Map<TrackDto>(newTrack);

            return Ok(_linkService.CreateLinks(updatedTrack));
        }

        [HttpDelete("{id}", Name = "DeleteTrack")]
        public async Task<IActionResult> Delete(int id)
        {
            var track = await _trackRepo.GetByIdAsync(id);

            if(track == null)
            {
                return NotFound();
            }

            _trackRepo.Delete(track);

            var mappedTrack = _mapper.Map<TrackDto>(track);
            

            return Ok(_linkService.CreateLinks(mappedTrack));
        }
    }

}
