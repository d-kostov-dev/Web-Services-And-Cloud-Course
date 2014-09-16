namespace StudentSystem.Services.Models
{
    using System;
    using System.Linq.Expressions;

    using StudentsSystem.Model;

    public class CourseModel
    {
        public static Expression<Func<Course, CourseModel>> PrepareModel
        {
            get
            {
                return x => new CourseModel()
                {
                    ID = x.ID,
                    Name = x.Name
                };
            }
        }

        public int ID { get; set; }

        public string Name { get; set; }
    }
}