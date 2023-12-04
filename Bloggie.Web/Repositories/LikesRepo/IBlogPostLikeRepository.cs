using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.LikesRepo
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesAsync(Guid blogPostId);

        Task<IEnumerable<BlogPostsLike>> GetLikesByBlog(Guid blogPostId);

        Task<BlogPostsLike> AddLikeForBLog(BlogPostsLike blogPostLike);
    }
}
