using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class AlbumDto : LinkedResourceBaseDto
    {
        public int AlbumId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string ImgUri { get; set; }

        public ICollection<TrackDto> Tracks { get; set; }


    }
}
