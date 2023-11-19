using Bloggie.Web.Data;
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


        public async Task<int> GetTotalLikesAsync(Guid blogPostId)
        {
            return await _context.BlogPostsLike.CountAsync(x => x.BlogPostsId.Equals(blogPostId));
        }
    }
}
