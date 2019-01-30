using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
