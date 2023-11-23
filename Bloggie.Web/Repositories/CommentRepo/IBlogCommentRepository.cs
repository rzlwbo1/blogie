using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.CommentRepo
{
    public interface IBlogCommentRepository
    {
        Task<BlogPostsComment> AddCommentAsync(BlogPostsComment blogComment);

        Task<List<BlogPostsComment>> GetAllCommentsByBlogId(Guid id);
    }
}
