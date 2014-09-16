namespace StudentSystem.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using StudentsSystem.Data.Interfaces;
    using StudentsSystem.Data.Repositories;
    using StudentsSystem.Model;
    using StudentSystem.Services.Models;
    
    public class HomeworksController : BaseApiController
    {
        public HomeworksController()
            :this(new DbData())
        {
        }
        public HomeworksController(IDbData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var allItems = this.data.Homeworks
                .All()
                .Select(HomeworkModel.PrepareModel);

            return Ok(allItems);
        }

        [HttpPost]
        public IHttpActionResult Create(HomeworkModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = new Homework()
            {
                Content = item.Content,
            };

            this.data.Homeworks.Add(newItem);
            this.data.Homeworks.SaveChanges();

            item.ID = newItem.ID;

            return Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, HomeworkModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItem = this.data.Homeworks.Find(id);

            if (existingItem == null)
            {
                return BadRequest("Invalid Course");
            }

            // Check if some of the fields in the new data are not null
            existingItem.Content = item.Content;
            this.data.Courses.SaveChanges();

            item.ID = existingItem.ID;
            return Ok(item);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingItem = this.data.Homeworks.Find(id);
            if (existingItem == null)
            {
                return BadRequest("Such Item does not exists!");
            }

            this.data.Homeworks.Delete(existingItem);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
