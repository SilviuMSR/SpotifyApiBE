﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos.ResourceParameters
{
    public class PlaylistAlbumResourceParameters : BaseResourceParameters
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}
