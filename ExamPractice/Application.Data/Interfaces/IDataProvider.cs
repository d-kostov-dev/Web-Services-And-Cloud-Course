namespace Application.Data.Interfaces
{
    using Application.Models;

    public interface IDataProvider
    {
        IRepository<TestEntity> TestEntities { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
