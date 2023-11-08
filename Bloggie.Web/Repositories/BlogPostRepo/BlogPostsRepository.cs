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

        public async Task<BlogPosts?> DeleteAsync(Guid id)
        {
            var foundBLog = await _context.BlogPosts.FindAsync(id);

            if (foundBLog != null)
            {
                _context.BlogPosts.Remove(foundBLog);
                await _context.SaveChangesAsync();
                return foundBLog;
            }

            return null;
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

        public async Task<BlogPosts?> UpdateAsync(BlogPosts blog)
        {
            var existingBlog = await _context.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blog.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blog.Id;
                existingBlog.Heading = blog.Heading;
                existingBlog.Author = blog.Author;
                existingBlog.PageTitle = blog.PageTitle;
                existingBlog.Content = blog.Content;
                existingBlog.FeaturedImageUrl = blog.FeaturedImageUrl;
                existingBlog.ShortDescription = blog.ShortDescription;
                existingBlog.PublishDate = blog.PublishDate;
                existingBlog.UrlHandle = blog.UrlHandle;
                existingBlog.Visible = blog.Visible;
                existingBlog.Tags = blog.Tags;

                await _context.SaveChangesAsync();
                return existingBlog;

            }

            return null;

        }
    }
}
