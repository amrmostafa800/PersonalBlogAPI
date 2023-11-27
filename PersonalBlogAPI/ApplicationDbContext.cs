using Microsoft.EntityFrameworkCore;
using PersonalBlogAPI.Models;

namespace PersonalBlogAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }

    }
}