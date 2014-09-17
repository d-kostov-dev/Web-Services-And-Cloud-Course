namespace MusicCatalog.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;
    using MusicCatalog.Data.Interfaces;
    using MusicCatalog.Data.Migrations;
    using MusicCatalog.Models;

    public class MusicCatalogDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public MusicCatalogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicCatalogDbContext, Configuration>());
        }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Artist> Artists { get; set; }

        public IDbSet<Song> Songs { get; set; }

        public static MusicCatalogDbContext Create()
        {
            return new MusicCatalogDbContext();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
