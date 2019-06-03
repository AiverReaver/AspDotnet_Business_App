using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessApp.API.Helpers;
using BusinessApp.API.Models;

namespace BusinessApp.API.Data
{
    public interface IBusinessRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(PageParams pageParams);
        Task<User> GetUser(int id);
        Task<IEnumerable<Business>> GetUserBusinesses(int id);
        Task<Business> GetUserBusiness(int userId, int id);
        Task<PagedList<Business>> GetBusinesses(PageParams pageParams);
        Task<Business> GetBusiness(int id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetPhotoForBusiness(int businessId);
        Task<Video> GetVideo(int id);
    }
}