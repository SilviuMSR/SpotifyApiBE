using SpotifyApi.Domain.Models;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Services
{
    public interface IAlbmRepo : IRepo<Album>
    {
        Task<Album> GetAlbumByNameAsync(string albumName);
    }
}
