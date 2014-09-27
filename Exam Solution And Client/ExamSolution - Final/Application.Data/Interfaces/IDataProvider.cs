namespace Application.Data.Interfaces
{
    using Application.Models;

    public interface IDataProvider
    {
        IRepository<TestEntity> TestEntities { get; }

        IRepository<Game> Games { get; }

        IRepository<Guess> Guesses { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
