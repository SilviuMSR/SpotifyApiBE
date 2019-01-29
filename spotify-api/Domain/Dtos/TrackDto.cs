using System.Collections.Generic;

namespace SpotifyApi.Domain.Dtos
{
    public class TrackDto : LinkedResourceBaseDto
    {
        public int TrackId { get; set; }

        public string Name { get; set; }

        public string Href { get; set; }

        public string PreviewUrl { get; set; }

        public ICollection<ArtistDto> Artists { get; set; }

    }
}
