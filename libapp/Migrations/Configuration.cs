using libapp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace libapp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "librarian" };
            var role3 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "administrator@gmail.com", UserName = "administrator@gmail.com" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);

            var librarian = new ApplicationUser { Email = "librarian@gmail.com", UserName = "librarian@gmail.com" };
            string password1 = "H4!b5at+kWls";
            var result1 = userManager.Create(librarian, password1);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role3.Name);

                userManager.AddToRole(librarian.Id, role2.Name);
                userManager.AddToRole(librarian.Id, role3.Name);
            }

            base.Seed(context);
        }
    }
}
