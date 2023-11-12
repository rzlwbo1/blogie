using Bloggie.Web.Repositories.ImagesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsyc(IFormFile file)
        {
            // call a repo
            var imageUrl = await imageRepository.UploadAsyc(file);

            if (imageUrl == null)
            {
                return Problem("Imge Upload not succesfull", null, (int?)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new {link = imageUrl});
        }
    }
}
