using Bloggie.Web.Models.ViewModel;
using Bloggie.Web.Repositories.UsersRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public AdminUsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<IActionResult> Index()
        {
            var getUsersList = await userRepository.GetAllUsers();

            UserVm userVm = new UserVm();

            foreach (var user in getUsersList)
            {
                UserInfoVm userInfoVm = new UserInfoVm()
                {
                    Id = Guid.Parse(user.Id),
                    EmailAddress = user.Email,
                    Username = user.UserName,
                };

                userVm.Users.Add(userInfoVm);
            }

            ViewData["active"] = "showUsers";
            return View(userVm);
        }
    }
}
