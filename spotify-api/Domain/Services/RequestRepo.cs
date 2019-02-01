using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyApi.Domain.Dtos.ResourceParameters;
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
            _context.Remove(t);
            _context.SaveChanges();
        }

        public Task<List<Request>> FilterRequestByLocation(string ip)
        {
            return _context.Requests
                .Include(request => request.UserAgent)
                .Where(request => request.Source == ip)
                .OrderBy(request => request.Method)
                .ToListAsync();
        }

        public Task<List<Request>> GetAllAsync()
        {
            return _context.Requests
                .Include(request => request.UserAgent)
                .ToListAsync();
        }

        public PagedList<Request> GetAllPaginationAsync(RequestResourceParameters resourceParams)
        {
            var collectionBeforPaging = _context.Requests
                .Include(r => r.UserAgent)
                .AsQueryable();

            //filter by Method if type exists
            if (!string.IsNullOrEmpty(resourceParams.Method))
            {
                collectionBeforPaging = collectionBeforPaging
                    .Where(a => a.Method == resourceParams.Method);
            }

            //filter by Destination if type exists
            if (!string.IsNullOrEmpty(resourceParams.Destination))
            {
                collectionBeforPaging = collectionBeforPaging
                    .Where(a => a.Destination == resourceParams.Destination);
            }

            //filter by Source if type exists
            if (!string.IsNullOrEmpty(resourceParams.Source))
            {
                collectionBeforPaging = collectionBeforPaging
                    .Where(a => a.Source == resourceParams.Source);
            }

            return PagedList<Request>.Create(collectionBeforPaging, resourceParams.PageNumber, resourceParams.PageSize);
        }

        public Task<Request> GetByIdAsync(int id)
        {
            return _context.Requests
                .Include(request => request.UserAgent)
                .FirstOrDefaultAsync(request => request.RequestId == id);
        }

        public void Update(int id, Request t)
        {
            var request = _context.Requests
                .Include(r => r.UserAgent)
                .FirstOrDefault(r => r.RequestId == id);
            
            _context.Requests.Update(request);

            _context.SaveChanges();
        }
    }
}
