namespace Bloggie.Web.Repositories.ImagesRepo
{
    public interface IImageRepository
    {
        Task<string> UploadAsyc(IFormFile file);
    }
}
