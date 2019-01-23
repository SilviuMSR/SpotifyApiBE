using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class ArtistDto
    {
        public int ArtistId { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string ImgUri { get; set; }

        public List<Link> Links { get; set; }

    }
}
