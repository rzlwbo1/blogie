using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories.TagRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewData["active"] = "addTag";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Add")]
        public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
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

            var tag = await tagRepository.AddAsync(newTag);

            if(tag != null)
            {
                TempData["msg"] = "Succes Adding Tag";
                return RedirectToAction("List");
            }

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            // read db conttext
            var tags = await tagRepository.GetAllAsync();

            ViewData["active"] = "showTag";
            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var foundTag = await tagRepository.GetAsync(id);

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
            
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTagRequest editTag)
        {
            var editedTag = new Tag()
            {
                Id = editTag.Id,
                Name = editTag.Name,
                DisplayName = editTag.DisplayName,
            };

            try
            {
               await tagRepository.UpdateAsync(editedTag);
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
        public async Task<IActionResult> DeleteTag(EditTagRequest editTag)
        {
            try
            {
                var tag = await tagRepository.DeleteAsync(editTag.Id);

                if (tag != null)
                {
                    TempData["msg"] = "Succes to Delete";
                    return RedirectToAction("List");
                }

            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                TempData["msg"] = $"Failed to Delete {e.Message}";
            }

            return RedirectToAction("Edit", new {id = editTag.Id});

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOuter()
        {

            Guid id = new Guid(Request.Form["idTag"].ToString());
            try
            {
                var tag = await tagRepository.DeleteAsync(id);

                if (tag != null)
                {
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
