using SpotifyApi.Domain.Dtos;

namespace SpotifyApi.Domain.Logic.Links
{
    public interface ILinkService<T> where T : LinkedResourceBaseDto
    {
        T CreateLinks(T t);
        string CreateResourceUri(ResourceParameters resourceParameters, ResourceType type);
    }
}
