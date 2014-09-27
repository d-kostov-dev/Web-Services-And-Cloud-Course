namespace Application.Data.Interfaces
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Application.Models;

    public interface IDbContext
    {
        IDbSet<TestEntity> TestEntities { get; set; }

        int SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
