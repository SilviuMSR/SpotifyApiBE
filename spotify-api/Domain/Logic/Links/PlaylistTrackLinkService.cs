﻿using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Links
{
    public class PlaylistTrackLinkService : ILinkService<PlaylistTrackDto>
    {
        private readonly IUrlHelper _urlHelper;

        public PlaylistTrackLinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public PlaylistTrackDto CreateLinks(PlaylistTrackDto t)
        {
            t.Links.Add(new Link(_urlHelper.Link("GetPlaylistTracks",
            new { }),
            "get_all",
            "GET"));

            t.Links.Add(new Link(_urlHelper.Link("CreatePlaylistTrack",
              new { }),
              "post_playlistTrack",
              "POST"));

            return t;
        }

        public string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type)
        {
            switch (type)
            {
                case ResourceType.PreviousPage:
                    return _urlHelper.Link("GetPlaylistTracks",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceType.NextPage:
                    return _urlHelper.Link("GetPlaylistTracks",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetPlaylistTracks",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });
            }
        }
    }
}