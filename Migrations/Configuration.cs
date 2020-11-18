namespace Trippy_Land.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Trippy_Land.Models.Trippy_Land_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Trippy_Land.Models.Trippy_Land_Context context)
        {
           
        }
    }
}
