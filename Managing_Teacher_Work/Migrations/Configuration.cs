using System.Data.Entity.Migrations;

namespace Managing_Teacher_Work.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Managing_Teacher_Work.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Managing_Teacher_Work.Models.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
