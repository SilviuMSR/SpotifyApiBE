﻿using SpotifyApi.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class PlaylistAlbumDto : LinkedResourceBaseDto
    {
        public int PlaylistAlbumId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        public string ImgUri { get; set; }

        public IEnumerable<PlaylistTrackDto> Tracks { get; set; }
    }
}
