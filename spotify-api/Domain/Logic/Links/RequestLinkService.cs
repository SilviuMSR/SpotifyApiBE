using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Links
{
    public class RequestLinkService : ILinkService<RequestDto>
    {
        private readonly IUrlHelper _urlHelper;

        public RequestLinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public RequestDto CreateLinks(RequestDto t)
        {
            t.Links.Add(new Link(_urlHelper.Link("GetRequests",
              new { }),
              "get_all_requests",
              "GET"));


            t.Links.Add(new Link(_urlHelper.Link("GetRequestById",
               new { id = t.RequestId }),
              "get_request_by_id",
              "GET"));

            return t;
        }

        public string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type)
        {
            switch (type)
            {
                case ResourceType.PreviousPage:
                    return _urlHelper.Link("GetRequests",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceType.NextPage:
                    return _urlHelper.Link("GetRequests",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetRequests",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });
            }
        }
    }
}
