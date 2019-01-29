using SpotifyApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Logic.Links
{
    public interface ILinkService<T> where T : LinkedResourceBaseDto
    {
        T CreateLinks(T t);
        string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type);
    }
}
