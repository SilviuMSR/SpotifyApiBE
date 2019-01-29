﻿using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Links
{
    public class PlaylistAlbumLinkService : ILinkService<PlaylistAlbumDto>
    {
        private readonly IUrlHelper _urlHelper;

        public PlaylistAlbumLinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public PlaylistAlbumDto CreateLinks(PlaylistAlbumDto t)
        {

            t.Links.Add(new Link(_urlHelper.Link("GetPlaylistAlbums",
               new { }),
               "get_all",
               "GET"));

            t.Links.Add(new Link(_urlHelper.Link("CreatePlaylistAlbum",
              new { }),
              "post_playlistAlbum",
              "POST"));

            return t;
        }

        public string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type)
        {
            switch (type)
            {
                case ResourceType.PreviousPage:
                    return _urlHelper.Link("GetPlaylistAlbums",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceType.NextPage:
                    return _urlHelper.Link("GetPlaylistAlbums",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetPlaylistAlbums",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });
            }
        }
    }
}