using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Domain.Dtos;
using SpotifyApi.Domain.Dtos.ResourceParameters;
using System;

namespace SpotifyApi.Domain.Logic.Links
{
    public class TrackLinkService : ILinkService<TrackDto, TrackResourceParameters>
    {
        private readonly IUrlHelper _urlHelper;

        public TrackLinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public TrackDto CreateLinks(TrackDto track)
        {

            track.Links.Add(new Link(_urlHelper.Link("GetTracks",
                new { }),
                "get_all",
                "GET"));

            track.Links.Add(new Link(_urlHelper.Link("DeleteTrack",
             new { id = track.TrackId }),
             "delete_track",
             "DELETE"));


            track.Links.Add(new Link(_urlHelper.Link("GetTrackById",
             new { id = track.TrackId }),
             "self",
             "GET"));


            track.Links.Add(new Link(_urlHelper.Link("UpdateTrack",
             new { id = track.TrackId }),
             "update_self",
             "PUT"));

            return track;
        }

        public string CreateResourceUri(TrackResourceParameters resourceParameters,
                ResourceType type)
        {
            switch (type)
            {
                case ResourceType.PreviousPage:
                    return _urlHelper.Link("GetTracks",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber - 1,
                            pageSize = resourceParameters.PageSize
                        });
                case ResourceType.NextPage:
                    return _urlHelper.Link("GetTracks",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber + 1,
                            pageSize = resourceParameters.PageSize
                        });
                default:
                    return _urlHelper.Link("GetTracks",
                        new
                        {
                            pageNumber = resourceParameters.PageNumber,
                            pageSize = resourceParameters.PageSize
                        });
            }

        }

    }
}
