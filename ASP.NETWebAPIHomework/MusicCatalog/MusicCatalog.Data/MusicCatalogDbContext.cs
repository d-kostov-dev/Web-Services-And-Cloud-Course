namespace MusicCatalog.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using MusicCatalog.Data.Migrations;
    using MusicCatalog.Models;
    
    public class MusicCatalogDbContext : IdentityDbContext<ApplicationUser>
    {
        public MusicCatalogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicCatalogDbContext, Configuration>());
        }

        public static MusicCatalogDbContext Create()
        {
            return new MusicCatalogDbContext();
        }
    }
}
