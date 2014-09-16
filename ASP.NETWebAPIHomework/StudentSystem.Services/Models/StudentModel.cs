namespace StudentSystem.Services.Models
{
    using System;
    using System.Linq.Expressions;

    //using Antlr.Runtime.Misc;

    using StudentsSystem.Model;

    public class StudentModel
    {
        public static Expression<Func<Student, StudentModel>> FromStudent
        {
            get
            {
                return s => new StudentModel()
                {
                    ID = s.ID,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Number = s.Number,
                };
            }
        }

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Number { get; set; }
    }
}