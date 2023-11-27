using Microsoft.EntityFrameworkCore;
using PersonalBlogAPI.Models;

namespace PersonalBlogAPI.Repositories
{
    public class BlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            return await _context.SaveChangesAsync() != 0; // check if write more than 0 = add success;
        }

        public async Task<bool> Update(Blog blog)
        {
            _context.Blogs.Update(blog);
            return await _context.SaveChangesAsync() != 0; // check if write more than 0 = add success;
        }

        public async Task<bool> Delete(int blogId)
        {
            return await _context.Blogs.Where(b => b.Id == blogId).ExecuteDeleteAsync() != 0; // check if write more than 0 = add success;
        }

        public IEnumerable<Blog> GetAll()
        {
            return _context.Blogs;
        }

        public async Task<Blog> GetById(int id) => await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
    }
}
