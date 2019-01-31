using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using SpotifyApi.Domain.Services;

namespace SpotifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepo _requestRepo;


        public RequestController(IRequestRepo requestRepo)
        {
            _requestRepo = requestRepo;
        }

        // GET: api/Request
        [HttpGet(Name = "GetRequests")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {

            //task: add dto and links to previous next apges
            var requests = await _requestRepo.GetAllAsync();

            return Ok(requests);
        }
    }
}
