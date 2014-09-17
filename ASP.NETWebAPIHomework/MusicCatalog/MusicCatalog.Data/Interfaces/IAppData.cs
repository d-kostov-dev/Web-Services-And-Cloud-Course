namespace MusicCatalog.Data.Interfaces
{
    using MusicCatalog.Models;

    public interface IAppData
    {
        IRepository<Album> Albums { get; }

        IRepository<Song> Songs { get; }

        IRepository<Artist> Artists { get; }

        int SaveChanges();
    }
}
