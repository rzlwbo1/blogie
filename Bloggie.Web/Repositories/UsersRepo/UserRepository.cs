using Bloggie.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.UsersRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }


        public async Task<IEnumerable<IdentityUser>> GetAllUsers()
        {
            var listUsers = await authDbContext.Users.ToListAsync();

            // avoid display super admin user
            var superAdmin = await authDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals("superadmin@mail.com"));
            listUsers.Remove(superAdmin);

            return listUsers;
        }
    }
}
