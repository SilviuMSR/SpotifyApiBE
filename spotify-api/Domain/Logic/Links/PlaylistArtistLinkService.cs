﻿using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Links
{
    public class PlaylistArtistLinkService : ILinkService<PlaylistArtistDto>
    {
        private readonly IUrlHelper _urlHelper;

        public PlaylistArtistLinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public PlaylistArtistDto CreateLinks(PlaylistArtistDto t)
        {
            t.Links.Add(new Link(_urlHelper.Link("GetPlaylistArtists",
             new { }),
            "get_all",
            "GET"));

            t.Links.Add(new Link(_urlHelper.Link("CreatePlaylistArtist",
              new { }),
              "post_playlistArtist",
              "POST"));

            return t;
        }

        public string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type)
        {
            switch (type)
            {
                case ResourceType.PreviousPage:
                    return _urlHelper.Link("GetPlaylistArtists",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceType.NextPage:
                    return _urlHelper.Link("GetPlaylistArtists",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetPlaylistArtists",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });
            }
        }
    }
}