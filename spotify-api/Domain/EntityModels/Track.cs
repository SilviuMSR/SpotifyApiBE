using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Models
{
    public class Track
    {
        public int TrackId { get; set; }

        public string Name { get; set; }

        public string Href { get; set; }

        public string PreviewUrl { get; set; }

        public ICollection<Artist> Artists { get; set; }

        //navigation property to albums
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
