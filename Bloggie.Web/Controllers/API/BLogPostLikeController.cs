using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories.LikesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BLogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository likeRepository;

        public BLogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.likeRepository = blogPostLikeRepository;
        }



        [HttpPost, Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLike)
        {

            BlogPostsLike newLikeBLog = new BlogPostsLike
            {
                BlogPostsId = addLike.BlogPostId,
                UserId = addLike.UserId,
            };

            var result = await likeRepository.AddLikeForBLog(newLikeBLog);

            return Ok(result);

        }


        [HttpGet, Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesBlog([FromRoute] Guid blogPostId)
        {

            int likesTotal = await likeRepository.GetTotalLikesAsync(blogPostId);

            return Ok(likesTotal);

        }

    }
}
