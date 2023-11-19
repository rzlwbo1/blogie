namespace Bloggie.Web.Repositories.LikesRepo
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesAsync(Guid blogPostId);
    }
}
