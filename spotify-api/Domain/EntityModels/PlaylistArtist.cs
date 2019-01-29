﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.EntityModels
{
    public class PlaylistArtist
    {
        public int PlaylistArtistId { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string ImgUri { get; set; }

        public ICollection<PlaylistTrack> Tracks { get; set; }

    }
}