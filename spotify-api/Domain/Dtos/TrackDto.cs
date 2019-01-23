using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class TrackDto
    {
        public int TrackId { get; set; }

        public string Name { get; set; }

        public string Href { get; set; }

        public string PreviewUrl { get; set; }

        public ICollection<ArtistDto> Artists { get; set; }

        public List<Link> Links { get; set; }
    }
}
