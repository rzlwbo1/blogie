using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.LikesRepo
{
    public class BLogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext _context;

        public BLogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this._context = bloggieDbContext;
        }

        public async Task<BlogPostsLike> AddLikeForBLog(BlogPostsLike blogPostLike)
        {

            await _context.BlogPostsLike.AddAsync(blogPostLike);
            await _context.SaveChangesAsync();
            return blogPostLike;

        }

        public async Task<int> GetTotalLikesAsync(Guid blogPostId)
        {
            return await _context.BlogPostsLike.CountAsync(x => x.BlogPostsId.Equals(blogPostId));
        }
    }
}
