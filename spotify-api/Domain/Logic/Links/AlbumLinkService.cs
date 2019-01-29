using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Links
{
    public class AlbumLinkService : ILinkService<AlbumDto>
    {
        private readonly IUrlHelper _urlHelper;

        public AlbumLinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public AlbumDto CreateLinks(AlbumDto t)
        {
            t.Links.Add(new Link(_urlHelper.Link("GetAlbums",
               new { }),
               "get_all",
               "GET"));

            t.Links.Add(new Link(_urlHelper.Link("GetAlbumById",
              new { id = t.AlbumId }),
              "self",
              "GET"));

            t.Links.Add(new Link(_urlHelper.Link("DeleteAlbum",
            new { id = t.AlbumId }),
              "delete_album",
              "DELETE"));

            t.Links.Add(new Link(_urlHelper.Link("UpdateAlbum",
             new { id = t.AlbumId }),
            "update_self",
            "PUT"));

            return t;
        }

        public string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type)
        {
            switch (type)
            {
                case ResourceType.PreviousPage:
                    return _urlHelper.Link("GetAlbums",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceType.NextPage:
                    return _urlHelper.Link("GetAlbums",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetAlbums",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });
            }
        }
    }
}
