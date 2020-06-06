namespace NTier.Model.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NTier.Model.Context.ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NTier.Model.Context.ProjectContext context)
        {
            context.Users.AddOrUpdate(u => u.Name,
                new AppUser
                {
                    Name = "admin",
                    SurName = "admin",
                    UserName = "admin",
                    Password = "admin",
                    Role = Role.Admin,
                    Email = "admin@admin.com"
                });
        }
    }
}
