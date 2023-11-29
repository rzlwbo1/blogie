using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Repositories.UsersRepo
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsers();
    }
}
