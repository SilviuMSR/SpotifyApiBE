using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyApi.Domain.Services
{
    public interface IRepo<T> where T : class
    {
        void Add(T t);
        void Delete(T t);
        void Update(int id, T t);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        
    }
}
