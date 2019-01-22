using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class Link
    {
        public string Href { get; set; }

        public string Ref { get; set; }

        public string Method { get; set; }
    }
}
