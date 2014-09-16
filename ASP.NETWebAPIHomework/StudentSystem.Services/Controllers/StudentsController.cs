namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using StudentsSystem.Data.Interfaces;
    using StudentsSystem.Data.Repositories;
    using StudentsSystem.Model;
    using StudentSystem.Services.Models;
    
    public class StudentsController : BaseApiController
    {
        public StudentsController()
            :this(new DbData())
        {
        }
        public StudentsController(IDbData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var allStudents = this.data.Students
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

            this.data.Students.Add(newStudent);
            this.data.Students.SaveChanges();

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

            var existingStudent = this.data.Students.All().FirstOrDefault(x => x.ID == id);

            if (existingStudent == null)
            {
                return BadRequest("Invalid Student");
            }

            // Check if some of the fields in the new data are not null
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Number = student.Number;
            this.data.Students.SaveChanges();

            student.ID = existingStudent.ID;
            return Ok(student);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingStudent = this.data.Students.All().FirstOrDefault(a => a.ID == id);
            if (existingStudent == null)
            {
                return BadRequest("Such student does not exists!");
            }

            this.data.Students.Delete(existingStudent);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
