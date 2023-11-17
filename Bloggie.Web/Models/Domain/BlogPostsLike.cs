namespace Bloggie.Web.Models.Domain
{
    public class BlogPostsLike
    {
        public Guid Id { get; set; }
        public Guid BlogPostsId { get; set; }
        public Guid UserId { get; set; }
    }
}
