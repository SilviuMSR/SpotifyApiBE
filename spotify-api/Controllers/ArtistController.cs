using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Models;
using SpotifyApi.Domain.Services;
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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepo _artistRepo;
        private readonly IMapper _mapper;
 
        public ArtistController(IArtistRepo artistRepo, IMapper mapper)
        {
            _artistRepo = artistRepo;
            _mapper = mapper;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var artists = await _artistRepo.GetAllAsync();
            var mappedArtists = _mapper.Map<IEnumerable<Artist>>(artists);

            return Ok(mappedArtists);
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);

            _artistRepo.Add(artist);

            var mappedArtist = _mapper.Map<ArtistDto>(artist);

            return StatusCode(201);
        }

        //get an artist by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var artist = await _artistRepo.GetByIdAsync(id);

            if(artist == null)
            {
                return NotFound();
            }

            var mappedArtist = _mapper.Map<ArtistDto>(artist);

            return Ok(mappedArtist);

        }


        //delete a specific artists
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var artist = await _artistRepo.GetByIdAsync(id);

            if(artist == null)
            {
                return NotFound();
            }

            _artistRepo.Delete(artist);

            var mappedArtist = _mapper.Map<ArtistDto>(artist);

            return Ok(mappedArtist);

        }


        //update a specific artist
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArtistDto artistDto)
        {

            var mappedArtist = _mapper.Map<Artist>(artistDto);

            _artistRepo.Update(id, mappedArtist);

            var updatedArtist = await _artistRepo.GetByIdAsync(id);

            var mappedUpdatedArtist = _mapper.Map<ArtistDto>(updatedArtist);

            return Ok(mappedUpdatedArtist);
        }

    }
}
