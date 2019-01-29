﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Logic.Links;
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
        private readonly ILinkService<PlaylistTrackDto> _linkService;

        public PlaylistTrackController(IPlaylistTrack playlistTrackRepo,
            IMapper mapper,
            ILinkService<PlaylistTrackDto> linkService)
        {
            _playlistTrackRepo = playlistTrackRepo;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetPlaylistTracks")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {
            var tracks = _playlistTrackRepo.GetAllPaginationAsync(resourceParameters.PageNumber, resourceParameters.PageSize);
            var mappedTracks = _mapper.Map<IEnumerable<PlaylistTrackDto>>(tracks);

            //construct links to previus+next page
            var previousPage = tracks.HasPrevious ?
               _linkService.CreateResourceUri(resourceParameters, ResourceType.PreviousPage) : null;

            var nextPage = tracks.HasNext ?
                _linkService.CreateResourceUri(resourceParameters, ResourceType.NextPage) : null;

            mappedTracks= mappedTracks.Select(track =>
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

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return Ok(mappedTracks);
        }

        [HttpPost(Name = "CreatePlaylistTrack")]
        public async Task<IActionResult> Post([FromBody] PlaylistTrackDto trackDto)
        {
            var track = _mapper.Map<PlaylistTrack>(trackDto);

            _playlistTrackRepo.Add(track);

            var mappedTrack = _mapper.Map<PlaylistTrackDto>(track);

            return Ok(_linkService.CreateLinks(mappedTrack));
        }
    }
}