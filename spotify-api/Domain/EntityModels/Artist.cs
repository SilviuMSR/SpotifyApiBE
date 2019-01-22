using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string ImgUri { get; set; }


        //Navigation property to track
        public int TrackId { get; set; }
        public Track Track { get; set; }
    }
}
