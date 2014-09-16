namespace StudentsSystem.Data.Interfaces
{
    using StudentsSystem.Model;

    public interface IDbData
    {
        IRepository<Student> Students { get; }

        IRepository<Course> Courses { get; }

        IRepository<Homework> Homeworks { get; }

        void SaveChanges();
    }
}
