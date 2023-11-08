using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories.BlogPostRepo;
using Bloggie.Web.Repositories.TagRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.ObjectModel;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostsRepository blogRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostsRepository blogRepository)
        {
            this.tagRepository = tagRepository;
            this.blogRepository = blogRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

             var blogReq = new AddBlogPostRequest()
            {
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.DisplayName,
                    Value = x.Id.ToString()
                })
            };

            return View(blogReq);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Add")]
        public async Task<IActionResult> AddPost(AddBlogPostRequest addBlogPost)
        {

            // map viewmodel to domain
            var BlogPost = new BlogPosts()
            {
                Heading = addBlogPost.Heading,
                PageTitle = addBlogPost.PageTitle,
                Content = addBlogPost.Content,
                ShortDescription = addBlogPost.ShortDescription,
                FeaturedImageUrl = addBlogPost.FeaturedImageUrl,
                UrlHandle = addBlogPost.UrlHandle,
                PublishDate = DateTime.Now,
                Author = addBlogPost.Author,
                Visible = addBlogPost.Visible,
            };

            // map tags from selected tags request
            var selectedTags = new List<Tag>();

            foreach (var tagId in addBlogPost.SelectedTags)
            {
                var convertId = Guid.Parse(tagId.ToString());
                var getTag = await tagRepository.GetAsync(convertId);

                if (getTag != null)
                {
                    selectedTags.Add(getTag);
                }
            }

            // assign selected to blogpost
            BlogPost.Tags = selectedTags;

            try
            {
                await blogRepository.AddAsync(BlogPost);
                TempData["msg"] = "Succes to adding post";

            }
            catch (Exception)
            {
                TempData["msg"] = "Failed To Adding Post";
                throw;
            }

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogList = await blogRepository.GetAllAsync();
            return View(blogList);
        }

    }
}
