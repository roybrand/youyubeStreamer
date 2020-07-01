using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using matrixYT.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("YoutubeConnectionString"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories {get; set;}
        public DbSet<Song> Songs {get; set;}
    }
}