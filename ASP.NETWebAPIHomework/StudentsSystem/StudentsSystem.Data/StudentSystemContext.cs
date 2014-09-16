namespace StudentsSystem.Data
{
    using System.Data.Entity;

    using StudentsSystem.Data.Interfaces;
    using StudentsSystem.Data.Migrations;
    using StudentsSystem.Model;

    public class StudentSystemContext : DbContext, IDbContext
    {
        public StudentSystemContext()
            : base("StudentSystemDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());
        }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Homework> Homeworks { get; set; }

        public virtual IDbSet<Student> Students { get; set; }

        public void SaveChanges()
        {
            base.SaveChanges();
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
