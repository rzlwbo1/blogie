using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.CommentRepo
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly BloggieDbContext _context;

        public BlogCommentRepository(BloggieDbContext bloggieDbContext)
        {
            this._context = bloggieDbContext;
        }


        public async Task<BlogPostsComment> AddCommentAsync(BlogPostsComment blogComment)
        {
            await _context.BlogPostsComment.AddAsync(blogComment);
            await _context.SaveChangesAsync();
            return blogComment;
        }


        public async Task<List<BlogPostsComment>> GetAllCommentsByBlogId(Guid id)
        {
            return await _context.BlogPostsComment.Where(x => x.BlogPostsId.Equals(id))
                .ToListAsync();
        }
    }
}
