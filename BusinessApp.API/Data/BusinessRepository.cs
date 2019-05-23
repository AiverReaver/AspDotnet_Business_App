using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Business>> GetBusinesses()
        {
            var businesses = await _context.Businesses
                                        .Include(p => p.Photos)
                                        .ToListAsync();

            return businesses;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<Photo> GetPhotoForUser(int businessId)
        {
            return await _context.Photos.Where(u => u.BusinessId == businessId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                                .Include(p => p.Photo)
                                .Include(b => b.Businesses)
                                .ThenInclude(p => p.Photos)
                                .FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users
                                    .Include(p => p.Photo)
                                    .Include(b => b.Businesses)
                                    .ThenInclude(p => p.Photos)
                                    .ToListAsync();

            return users;
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