﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.EntityModels
{
    public class PlaylistTrack
    {
        public int PlaylistTrackId { get; set; }

        public string Name { get; set; }

        public string Href { get; set; }

        public string PreviewUrl { get; set; }

    }
}