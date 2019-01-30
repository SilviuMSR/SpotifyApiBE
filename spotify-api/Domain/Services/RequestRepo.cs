using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyApi.Domain.EntityModels;
using SpotifyApi.Domain.Logic;

namespace SpotifyApi.Domain.Services
{
    public class RequestRepo : IRequestRepo
    {
        private readonly DataContext _context;

        public RequestRepo(DataContext context)
        {
            _context = context;
        }

        public void Add(Request t)
        {
            _context.Requests.Add(t);
            _context.SaveChanges();
        }

        public void Delete(Request t)
        {
            throw new NotImplementedException();
        }

        public Task<List<Request>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public PagedList<Request> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Request t)
        {
            throw new NotImplementedException();
        }
    }
}
