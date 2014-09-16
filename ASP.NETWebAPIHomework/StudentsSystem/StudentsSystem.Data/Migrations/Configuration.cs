namespace StudentsSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using StudentsSystem.Model;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentSystemContext context)
        {
            var coursecSharpOne = new Course()
            {
                ID = 1,
                Name = "C# 1",
            };

            var coursecSharpTwo = new Course()
            {
                ID = 2,
                Name = "C# 2",
            };

            var coursecSharpOOP = new Course()
            {
                ID = 3,
                Name = "C# OOP",
            };

            context.Courses.AddOrUpdate(coursecSharpOne);
            context.Courses.AddOrUpdate(coursecSharpTwo);
            context.Courses.AddOrUpdate(coursecSharpOOP);

            var studentOne = new Student()
            {
                ID = 1,
                FirstName = "Pesho",
                LastName = "Stamatov",
                Number = 123456,
            };

            var studentTwo = new Student()
            {
                ID = 2,
                FirstName = "Kencho",
                LastName = "Ivailov",
                Number = 567123,
            };

            context.Students.AddOrUpdate(studentOne);
            context.Students.AddOrUpdate(studentTwo);
        }
    }
}
