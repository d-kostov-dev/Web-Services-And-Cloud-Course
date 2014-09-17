namespace MusicCatalog.Data.Interfaces
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using MusicCatalog.Models;

    public interface IAppDbContext
    {
        IDbSet<Album> Albums { get; set; }

        IDbSet<Artist> Artists { get; set; }

        IDbSet<Song> Songs { get; set; }

        int SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
