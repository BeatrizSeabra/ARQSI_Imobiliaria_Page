namespace Imobiliaria.Migrations
{
    using Imobiliaria.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Imobiliaria.DAL.ImobiliariaDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Imobiliaria.DAL.ImobiliariaDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            using (var ctx = new ApplicationDbContext())
            {
                //Cria ou atualiza roles
                var roleStore = new RoleStore<IdentityRole>(ctx);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                if(!roleManager.RoleExists("Admin"))
                    roleManager.Create(new IdentityRole("Admin"));
                if(!roleManager.RoleExists("Cliente"))
                    roleManager.Create(new IdentityRole("Cliente"));

                //cria ou atualiza admin
                var userStore = new UserStore<ApplicationUser>(ctx);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ApplicationUser user;
                user=userManager.FindByEmail("admin@app.test");
                if(user==null){
                    user=new ApplicationUser();
                    user.Email="admin@app.test";
                    user.EmailConfirmed=true;
                    user.UserName = user.Email;
                    user.LockoutEnabled = false;
                    PasswordHasher ph=new PasswordHasher();
                    user.PasswordHash = ph.HashPassword("A-z123456");
                    userManager.Create(user);
                }
                userManager.AddToRole(user.Id, "Admin");
                userManager.AddToRole(user.Id, "Cliente");

                //grava
                ctx.SaveChanges();
            }
            
        }
    }
}
