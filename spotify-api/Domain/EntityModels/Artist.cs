﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Uri { get; set; }

        [StringLength(100)]
        public string ImgUri { get; set; }

    }
}
