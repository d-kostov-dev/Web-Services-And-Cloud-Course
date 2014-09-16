namespace StudentSystem.Services.Models
{
    using System;
    using System.Linq.Expressions;

    using StudentsSystem.Model;

    public class HomeworkModel
    {
        public static Expression<Func<Homework, HomeworkModel>> PrepareModel
        {
            get
            {
                return x => new HomeworkModel()
                {
                    ID = x.ID,
                    Content = x.Content,
                };
            }
        }

        public int ID { get; set; }

        public string Content { get; set; }
    }
}