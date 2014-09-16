namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using StudentsSystem.Data.Interfaces;
    using StudentsSystem.Data.Repositories;
    using StudentsSystem.Model;
    using StudentSystem.Services.Models;
    
    public class CoursesController : BaseApiController
    {
        public CoursesController()
            :this(new DbData())
        {
        }
        public CoursesController(IDbData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var allItems = this.data.Courses
                .All()
                .Select(CourseModel.PrepareModel);

            return Ok(allItems);
        }

        [HttpPost]
        public IHttpActionResult Create(CourseModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = new Course()
            {
                Name = item.Name
            };

            this.data.Courses.Add(newItem);
            this.data.Courses.SaveChanges();

            item.ID = newItem.ID;

            return Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, CourseModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItem = this.data.Courses.All().FirstOrDefault(x => x.ID == id);

            if (existingItem == null)
            {
                return BadRequest("Invalid Course");
            }

            // Check if some of the fields in the new data are not null
            existingItem.Name = item.Name;
            this.data.Courses.SaveChanges();

            item.ID = existingItem.ID;
            return Ok(item);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingItem = this.data.Courses.Find(id);
            if (existingItem == null)
            {
                return BadRequest("Such Item does not exists!");
            }

            this.data.Courses.Delete(existingItem);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
