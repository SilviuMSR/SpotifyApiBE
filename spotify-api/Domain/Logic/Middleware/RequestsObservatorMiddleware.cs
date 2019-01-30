using Microsoft.AspNetCore.Http;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Middleware
{
    public class RequestsObservatorMiddleware
    {
        private readonly RequestDelegate _next;
  

        public RequestsObservatorMiddleware(RequestDelegate next)
        {
            _next = next;
 
        }

        public async Task Invoke(HttpContext context, IRequestRepo requestRepo)
        {
            //code dealing with request
            //saving request data to dbcontext
            requestRepo.Add(new Request
            {
                Source = context.Connection.RemoteIpAddress.ToString() + ":" + context.Connection.RemotePort.ToString(),
                Destination = context.Request.Path.ToString(),
                Method = context.Request.Method,
            });

            await _next(context);

        }
    }
}
