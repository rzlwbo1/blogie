using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.LikesRepo
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesAsync(Guid blogPostId);

        Task<BlogPostsLike> AddLikeForBLog(BlogPostsLike blogPostLike);
    }
}
