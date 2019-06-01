using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessApp.API.Helpers;
using BusinessApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessApp.API.Data
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly DataContext _context;
        public BusinessRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Business> GetBusiness(int id)
        {
            var business = await _context.Businesses
                                    .Include(p => p.Photos)
                                    .Include(v => v.Video)
                                    .FirstOrDefaultAsync(b => b.Id == id);
            
            return business;
        }

        public async Task<PagedList<Business>> GetBusinesses(PageParams pageParams)
        {
            var businesses = _context.Businesses
                                .Include(p => p.Photos)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(pageParams.SearchQuery))
            {
                businesses = businesses
                                .Where(u => 
                                u.Name.ToLower().Contains(pageParams.SearchQuery.ToLower()) 
                                || u.Address.ToLower().Contains(pageParams.SearchQuery.ToLower()));
            }

            if (pageParams.UserId != 0)
            {
                businesses = businesses.Where(b => b.Id == pageParams.UserId);
            }

            return await PagedList<Business>.CreateAsync(businesses, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<Photo> GetPhotoForBusiness(int businessId)
        {
            return await _context.Photos.Where(u => u.BusinessId == businessId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                                .Include(p => p.PaytmOrders)
                                .Include(b => b.Businesses)
                                .ThenInclude(p => p.Photos)
                                .FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }

        public async Task<IEnumerable<Business>> GetUserBusinesses(int id)
        {
            var businesses = (await _context.Users
                                        .Include(b => b.Businesses)
                                        .ThenInclude(p => p.Photos)
                                        .FirstOrDefaultAsync(u => u.Id == id)).Businesses;

            return businesses;
        }

        public async Task<PagedList<User>> GetUsers(PageParams pageParams)
        {
            var users = _context.Users
                            .Include(b => b.Businesses)
                            .ThenInclude(p => p.Photos)
                            .AsQueryable();
            
            users = users.Where(u => u.Id != pageParams.UserId);

            users = users.Where(u => u.Gender == pageParams.Gender);

            return await PagedList<User>.CreateAsync(users, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Video> GetVideo(int id)
        {
            var photo = await _context.Videos.FirstOrDefaultAsync(v => v.Id == id);

            return photo;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}