﻿using SpotifyApi.Domain.Dtos.ResourceParameters;
using SpotifyApi.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Services
{
    public interface IPlaylistTrackRepo : IRepo<PlaylistTrack, PlaylistTrackResourceParameters>
    {
        bool GetByName(string name, string username);
    }
}
