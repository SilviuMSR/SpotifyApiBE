using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Links
{
    public class ArtistLinkService : ILinkService<ArtistDto>
    {
        private readonly IUrlHelper _urlHelper;

        public ArtistLinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public ArtistDto CreateLinks(ArtistDto t)
        {
            t.Links.Add(new Link(_urlHelper.Link("GetArtists",
                 new { }),
                 "self",
                 "GET"));

            t.Links.Add(new Link(_urlHelper.Link("DeleteArtist",
             new { id = t.ArtistId }),
             "delete_artist",
             "DELETE"));


            t.Links.Add(new Link(_urlHelper.Link("GetArtistById",
             new { id = t.ArtistId }),
             "self",
             "GET"));


            t.Links.Add(new Link(_urlHelper.Link("UpdateArtist",
             new { id = t.ArtistId }),
             "self",
             "PUT"));

            return t;
        }

        public string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type)
        {

            switch (type)
            {
                case ResourceType.PreviousPage:
                    return _urlHelper.Link("GetArtists",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceType.NextPage:
                    return _urlHelper.Link("GetArtists",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetArtists",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });
            }
        }
    }
}
