namespace Bloggie.Web.Models.Domain
{
    public class BlogPostsComment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid BlogPostsId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
