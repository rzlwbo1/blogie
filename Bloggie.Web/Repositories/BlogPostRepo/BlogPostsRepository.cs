using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.BlogPostRepo;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        private readonly BloggieDbContext _context;


        public BlogPostsRepository(BloggieDbContext bloggieDbContext)
        {
            this._context = bloggieDbContext;
        }



        public async Task<BlogPosts> AddAsync(BlogPosts blog)
        {
            await _context.BlogPosts.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public Task<BlogPosts?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPosts>> GetAllAsync()
        {
            return await _context.BlogPosts
                .Include(x => x.Tags)
                .ToListAsync();
        }

        public async Task<BlogPosts?> GetAsync(Guid id)
        {
            return await _context.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<BlogPosts?> UpdateAsync(BlogPosts tag)
        {
            throw new NotImplementedException();
        }
    }
}
