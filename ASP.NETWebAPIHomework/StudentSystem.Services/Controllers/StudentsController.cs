namespace StudentSystem.Services.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using StudentsSystem.Data.Interfaces;
    using StudentsSystem.Model;
    using StudentsSystem.Data.Repositories;
    using StudentSystem.Services.Models;


    public class StudentsController : ApiController
    {
        private IRepository<Student> students;

        public StudentsController()
            : this(new Repository<Student>())
        {
        }

        public StudentsController(IRepository<Student> studentsRepo)
        {
            this.students = studentsRepo;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var allStudents = this.students
                .All()
                .Select(StudentModel.FromStudent);

            return Ok(allStudents);
        }

        [HttpPost]
        public IHttpActionResult Create(StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStudent = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Number = student.Number,
            };

            this.students.Add(newStudent);
            this.students.SaveChanges();

            student.ID = newStudent.ID;

            return Ok(student);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, StudentModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = this.students.All().FirstOrDefault(x => x.ID == id);

            if (existingStudent == null)
            {
                return BadRequest("Invalid Student");
            }

            // Check if some of the fields in the new data are not null
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Number = student.Number;
            this.students.SaveChanges();

            student.ID = existingStudent.ID;
            return Ok(student);
        }
    }
}
