﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Models;
using SpotifyApi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using SpotifyApi.Domain.Dtos;
using System.Collections.Generic;
using SpotifyApi.Domain.Logic.Links;
using System.Linq;

namespace SpotifyApi.Controllers
{

    [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbmRepo _albumRepo;
        private readonly IMapper _mapper;
        private readonly ILinkService<AlbumDto> _linkService;


        public AlbumController(IAlbmRepo albumRepo, 
            IMapper mapper,
            ILinkService<AlbumDto> linkService)
        {
            _albumRepo = albumRepo;
            _mapper = mapper;
            _linkService = linkService;
        }


        [HttpGet(Name = "GetAlbums")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {
            var albums = _albumRepo.GetAllPaginationAsync(resourceParameters.PageNumber, resourceParameters.PageSize);
            var mappedAlbums = _mapper.Map<IEnumerable<AlbumDto>>(albums);

            //Construct links to previous+ next page
            var previousPage = albums.HasPrevious ?
                _linkService.CreateResourceUri(resourceParameters, ResourceType.PreviousPage) : null;

            var nextPage = albums.HasNext ?
                _linkService.CreateResourceUri(resourceParameters, ResourceType.NextPage) : null;

            mappedAlbums = mappedAlbums.Select(album =>
            {
                album = _linkService.CreateLinks(album);
                return album;
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

            return Ok(new
            {
                values = mappedAlbums,
                links = paginationMetadata
            });
        }

        [HttpGet("{id}", Name = "GetAlbumById")]
        public async Task<IActionResult> Get(int id)
        {
            var album = await _albumRepo.GetByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            var mappedAlbum = _mapper.Map<AlbumDto>(album);

            return Ok(_linkService.CreateLinks(mappedAlbum));
        }

        [HttpPost(Name = "CreateAlbum")]
        public async Task<IActionResult> Post([FromBody] AlbumDto albumDto)
        {
            //mapping dto to entity
            var album = _mapper.Map<Album>(albumDto);

            _albumRepo.Add(album);

            var mappedAlbum = _mapper.Map<AlbumDto>(album);
            
            return Ok(_linkService.CreateLinks(mappedAlbum));
        }

        [HttpDelete("{id}", Name = "DeleteAlbum")]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumRepo.GetByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            _albumRepo.Delete(album);

            var mappedAlbum = _mapper.Map<AlbumDto>(album);

            return Ok(_linkService.CreateLinks(mappedAlbum));
        }


        [HttpPut("{id}", Name = "UpdateAlbum")]
        public async Task<IActionResult> Update(int id, [FromBody] AlbumDto albumDto)
        {
            var album = _mapper.Map<Album>(albumDto);
            
            _albumRepo.Update(id, album);

            var updatedAlbum = await _albumRepo.GetByIdAsync(id);

            var mappedUpdatedAlbum = _mapper.Map<AlbumDto>(updatedAlbum);

            return Ok(_linkService.CreateLinks(mappedUpdatedAlbum));
        }

    }
}
