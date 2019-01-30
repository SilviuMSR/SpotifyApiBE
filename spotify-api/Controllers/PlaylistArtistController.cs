﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyApi.Domain.Dtos;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Logic.Links;
using SpotifyApi.Domain.Services;
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
        private readonly ILinkService<PlaylistArtistDto> _linkService;

        public PlaylistArtistController(IPlaylistArtist playlistArtistRepo, 
            IMapper mapper,
            ILinkService<PlaylistArtistDto> linkService)
        {
            _playlistArtistRepo = playlistArtistRepo;
            _mapper = mapper;
            _linkService = linkService;
        }

        [HttpGet(Name = "GetPlaylistArtists")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {
            var artists = _playlistArtistRepo.GetAllPaginationAsync(resourceParameters.PageNumber, resourceParameters.PageSize);
            var mappedArtists = _mapper.Map<IEnumerable<PlaylistArtistDto>>(artists);

            //construct links to previus+next page
            var previousPage = artists.HasPrevious ?
               _linkService.CreateResourceUri(resourceParameters, ResourceType.PreviousPage) : null;

            var nextPage = artists.HasNext ?
                _linkService.CreateResourceUri(resourceParameters, ResourceType.NextPage) : null;

            mappedArtists = mappedArtists.Select(artist =>
            {
                artist = _linkService.CreateLinks(artist);
                return artist;
            });

            var paginationMetadata = new
            {
                totalCount = artists.TotalCount,
                pageSize = artists.PageSize,
                currentPage = artists.CurrentPage,
                totalPages = artists.TotalPages,
                previousPageLink = previousPage,
                nextPageLink = nextPage
            };

            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(paginationMetadata));

            return Ok(mappedArtists);
        }

        [HttpPost(Name = "CreatePlaylistArtist")]
        public async Task<IActionResult> Post([FromBody] PlaylistArtistDto artistDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var artist = _mapper.Map<PlaylistArtist>(artistDto);

            _playlistArtistRepo.Add(artist);

            var mappedArtist = _mapper.Map<PlaylistArtistDto>(artist);

            return Ok(_linkService.CreateLinks(mappedArtist));
        }
    }
}
