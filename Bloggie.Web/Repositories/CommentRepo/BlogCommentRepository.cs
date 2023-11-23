using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

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
    }
}
