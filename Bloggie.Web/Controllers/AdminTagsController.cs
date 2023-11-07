using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        } 


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Add")]
        public IActionResult SubmitTag(AddTagRequest addTagRequest)
        {
            // Manually get request from form
            //var name = Request.Form["name"]; // name = value dalam attribue name html
            //var displayName = Request.Form["displayName"];


            // model binding pake request di parameter
            //string name = addTagRequest.Name;
            //string display = addTagRequest.DisplayName;

            Tag newTag = new()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            bloggieDbContext.Tags.Add(newTag);
            bloggieDbContext.SaveChanges();

            return RedirectToAction("Add");
        }


        [HttpGet]
        public IActionResult List()
        {
            // read db conttext
            var tags = bloggieDbContext.Tags.ToList();

            return View(tags);
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {

            var foundTag = bloggieDbContext.Tags.FirstOrDefault(t => t.Id == id);

            if (foundTag != null)
            {
                var editTag = new EditTagRequest()
                {
                    Id = id,
                    Name = foundTag.Name,
                    DisplayName = foundTag.DisplayName,
                };
                return View(editTag);
            }

            return View(null);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditTagRequest editTag)
        {
            var editedTag = new Tag()
            {
                Id = editTag.Id,
                Name = editTag.Name,
                DisplayName = editTag.DisplayName,
            };

            try
            {
                bloggieDbContext.Tags.Update(editedTag);
                bloggieDbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                TempData["msg"] = $"Failed to Update {e.Message}";
                return RedirectToAction("Edit", new {id = editedTag.Id});
            }

            return RedirectToAction("List");
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTag(EditTagRequest editTag)
        {
            try
            {
                var tag = bloggieDbContext.Tags.Find(editTag.Id);

                if (tag != null)
                {
                    bloggieDbContext.Tags.Remove(tag);
                    bloggieDbContext.SaveChanges();
                    TempData["msg"] = "Succes to Delete";
                }

            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                TempData["msg"] = $"Failed to Delete {e.Message}";
            }

            return RedirectToAction("List");

        }

    }
}
