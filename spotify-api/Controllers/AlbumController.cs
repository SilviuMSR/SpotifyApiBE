using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Models;
using SpotifyApi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using SpotifyApi.Domain.Dtos;
using System.Collections.Generic;

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


        public AlbumController(IAlbmRepo albumRepo, IMapper mapper)
        {
            _albumRepo = albumRepo;
            _mapper = mapper;
        }


        [HttpGet(Name = "Get Albums")]
        public async Task<IActionResult> Get()
        {
            var albums = await _albumRepo.GetAllAsync();
            var mappedAlbums = _mapper.Map<IEnumerable<AlbumDto>>(albums);

            return Ok(mappedAlbums);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var album = await _albumRepo.GetByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            var mappedAlbum = _mapper.Map<AlbumDto>(album);

            return Ok(mappedAlbum);
        }

        [HttpPost(Name = nameof(Post))]
        public async Task<IActionResult> Post([FromBody] AlbumDto albumDto)
        {
            //mapping dto to entity
            var album = _mapper.Map<Album>(albumDto);

            _albumRepo.Add(album);

            var mappedAlbum = _mapper.Map<AlbumDto>(album);

            mappedAlbum.Links = new List<Link>();

            mappedAlbum.Links.Add(new Link(this.Url.Link(nameof(Post), mappedAlbum.AlbumId).ToString() , "self", "POST"));

            return Created($"http://localhost:5000/api/album/{album.AlbumId}", mappedAlbum);
        }

        [HttpDelete("{id}", Name = "Delete Album")]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumRepo.GetByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            _albumRepo.Delete(album);

            var mappedAlbum = _mapper.Map<AlbumDto>(album);

            return Ok(mappedAlbum);
        }


        [HttpPut("{id}", Name = "Update Album")]
        public async Task<IActionResult> Update(int id, [FromBody] AlbumDto albumDto)
        {
            var album = _mapper.Map<Album>(albumDto);
            
            _albumRepo.Update(id, album);

            var updatedAlbum = await _albumRepo.GetByIdAsync(id);

            var mappedUpdatedAlbum = _mapper.Map<AlbumDto>(updatedAlbum);

            return Ok(mappedUpdatedAlbum);
        }

    }
}
