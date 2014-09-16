namespace StudentsSystem.Data.Interfaces
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using StudentsSystem.Model;

    public interface IDbContext
    {
       IDbSet<Course> Courses { get; set; }

       IDbSet<Homework> Homeworks { get; set; }

       IDbSet<Student> Students { get; set; }

       void SaveChanges();

       IDbSet<TEntity> Set<TEntity>() where TEntity : class;

       DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
