using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.BlogPostRepo
{
    public interface IBlogPostsRepository
    {
        Task<IEnumerable<BlogPosts>> GetAllAsync();
        Task<BlogPosts?> GetAsync(Guid id);
        Task<BlogPosts> AddAsync(BlogPosts blog);
        Task<BlogPosts?> UpdateAsync(BlogPosts blog);
        Task<BlogPosts?> DeleteAsync(Guid id);
    }
}
