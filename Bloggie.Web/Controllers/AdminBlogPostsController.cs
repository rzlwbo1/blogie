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
                return RedirectToAction("List");

            }
            catch (Exception)
            {
                TempData["msg"] = "Failed To Adding Post";
            }

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogList = await blogRepository.GetAllAsync();
            return View(blogList);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogRepository.GetAsync(id);
            var tags = await tagRepository.GetAllAsync();

            // karna ketika view form edit perluu menampilkan data tags, maka speerti inilah
            if (blogPost != null)
            {
                var EditBlogRequest = new EditBlogPostRequest()
                {
                    Id = id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Author = blogPost.Author,
                    PublishDate = DateTime.Now,
                    Visible = blogPost.Visible,
                    Tags = tags.Select(x => new SelectListItem
                    {
                        Text = x.DisplayName,
                        Value = x.Id.ToString(),
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(EditBlogRequest);

            }


            return NotFound();
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBLog(EditBlogPostRequest editBlogPost)
        {
            // map vm to model domain
            var BlogPosts = new BlogPosts
            {
                Id = editBlogPost.Id,
                Heading = editBlogPost.Heading,
                PageTitle = editBlogPost.PageTitle,
                Content = editBlogPost.Content,
                ShortDescription = editBlogPost.ShortDescription,
                UrlHandle = editBlogPost.UrlHandle,
                FeaturedImageUrl = editBlogPost.FeaturedImageUrl,
                Author = editBlogPost.Author,
                PublishDate = DateTime.Now,
                Visible = editBlogPost.Visible,

            };

            // map tags into domain
            var selectedTags = new List<Tag>();
            foreach (var tagInput in editBlogPost.SelectedTags)
            {
                if(Guid.TryParse(tagInput, out var tagId))
                {
                    var foundTag = await tagRepository.GetAsync(tagId);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            // assign to domain
            BlogPosts.Tags = selectedTags;

            // pass to repo
            var updatedBlog = await blogRepository.UpdateAsync(BlogPosts);
            if (updatedBlog != null)
            {
                TempData["msg"] = "Succes Update BLog";
                return RedirectToAction("List");
            }

            TempData["msg"] = "Failed Update BLog";
            return RedirectToAction("Edit", new {id = editBlogPost.Id});

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPost)
        {

            var deleted = await blogRepository.DeleteAsync(editBlogPost.Id);
            if (deleted != null)
            {
                TempData["msg"] = "Succes to Delete blog";
                return RedirectToAction("List");
            }

            TempData["msg"] = "Failed to Delete blog";
            return RedirectToAction("Edit", new { id = editBlogPost.Id });
        }

    }
}
