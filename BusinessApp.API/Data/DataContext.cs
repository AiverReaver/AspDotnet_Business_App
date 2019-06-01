using BusinessApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessApp.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<PaytmOrder> PaytmOrders { get; set; }
    }
}