using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Bloggie.Web.Repositories.ImagesRepo
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            account = new Account(
                _configuration.GetSection("Cloudinary")["CloudName"],
                _configuration.GetSection("Cloudinary")["ApiKey"],
                _configuration.GetSection("Cloudinary")["ApiSecret"]);
        }

        public async Task<string> UploadAsyc(IFormFile file)
        {
            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                //PublicId = "olympic_flag"
                DisplayName = file.Name,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
