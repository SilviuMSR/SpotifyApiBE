using SpotifyApi.Domain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Dtos
{
    public class ListDto
    {
        public IEnumerable<LinkedResourceBaseDto> Values { get; set; }
        public PagedList<LinkedResourceBaseDto> Links { get; set; }

    }
}
