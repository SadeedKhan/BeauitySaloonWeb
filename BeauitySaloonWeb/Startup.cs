using BeauitySaloonWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(BeauitySaloonWeb.Startup))]
namespace BeauitySaloonWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles("admin@admin.com", "Admin").Wait();

            // Create SalonManager
            CreateUserAndRoles("manager@manager.com", "Manager").Wait();

            // Create User
            CreateUserAndRoles("user@user.com").Wait();
        }

        // In this method we will create default User roles and Admin user for login    
        private static async Task CreateUserAndRoles(string email, string roleName = null)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };
            var password = WebConfigurationManager.AppSettings["Password"];

            if (roleName != null)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (result.Succeeded)
                        role = await roleManager.FindByNameAsync(roleName);
                }

                if (!UserManager.Users.Any(x => x.Roles.Any(y => y.RoleId == role.Id)))
                {
                    var result = await UserManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, roleName);
                    }
                }
            }
            else
            {
                if (!UserManager.Users.Any(x => x.Roles.Count() == 0))
                {
                    var result = await UserManager.CreateAsync(user, password);
                }
            }
        }
    }
}
