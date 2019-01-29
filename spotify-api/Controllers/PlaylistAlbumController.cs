using AutoMapper;
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
    public class PlaylistAlbumController : ControllerBase
    {

        private readonly IPlaylistAlbum _playlistAlbumRepo;
        private readonly IMapper _mapper;
        private readonly ILinkService<PlaylistAlbumDto> _linkService;

        public PlaylistAlbumController(IPlaylistAlbum playlistAlbumRepo, 
            IMapper mapper,
            ILinkService<PlaylistAlbumDto> linkService)
        {
            _playlistAlbumRepo = playlistAlbumRepo;
            _mapper = mapper;
            _linkService = linkService;
        }

        [HttpGet(Name = "GetPlaylistAlbums")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {
            var albums = _playlistAlbumRepo.GetAllPaginationAsync(resourceParameters.PageNumber, resourceParameters.PageSize);

            var mappedAlbums= _mapper.Map<IEnumerable<PlaylistAlbumDto>>(albums);

            //construct links to previus+next page
            var previousPage = albums.HasPrevious ?
               _linkService.CreateResourceUri(resourceParameters, ResourceType.PreviousPage) : null;

            var nextPage = albums.HasNext ?
                _linkService.CreateResourceUri(resourceParameters, ResourceType.NextPage) : null;

            mappedAlbums = mappedAlbums.Select(track =>
            {
                track = _linkService.CreateLinks(track);
                return track;
            });

            var paginationMetadata = new
            {
                totalCount = albums.TotalCount,
                pageSize = albums.PageSize,
                currentPage = albums.CurrentPage,
                totalPages = albums.TotalPages,
                previousPageLink = previousPage,
                nextPageLink = nextPage
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));



            return Ok(mappedAlbums);
        }

        [HttpPost(Name = "CreatePlaylistAlbum")]
        public async Task<IActionResult> Post([FromBody] PlaylistAlbumDto albumDto)
        {
            var album = _mapper.Map<PlaylistAlbum>(albumDto);

            _playlistAlbumRepo.Add(album);

            var mappedAlbum = _mapper.Map<PlaylistAlbumDto>(album);

            return Ok(_linkService.CreateLinks(mappedAlbum));
        }


    }
}
