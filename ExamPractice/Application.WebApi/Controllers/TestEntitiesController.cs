namespace Application.WebApi.Controllers
{
    using System.Web.Http;
    using System.Linq;

    using Application.Data.Interfaces;
    using Application.Models;
    using Application.WebApi.Models;
    using Application.WebApi.Providers;

    /// <summary>
    /// This controller is created for testing purposes only. Ignore it!
    /// </summary>
    public class TestEntitiesController : BaseController
    {
        public TestEntitiesController(IDataProvider dataProvider, IUserIdProvider userIdProvider)
            : base (dataProvider, userIdProvider)
        {
        }

        // Public
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var allItems = this.data.TestEntities
                .All()
                .Select(TestEntityModel.ViewModel);

            return this.Ok(allItems);
        }

        // Registered Users Only
        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(TestEntityModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var authorId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(authorId);

            if (currentUser == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "user"));
            }

            var newItem = new TestEntity()
            {
                Name = item.Name,
                User = currentUser,
            };

            this.data.TestEntities.Add(newItem);
            this.data.SaveChanges();

            item.Id = newItem.Id;
            item.UserName = newItem.User.UserName;

            return this.Ok(item);
        }

        // Registered Users Only
        [HttpPut]
        [Authorize]
        public IHttpActionResult Update(int id, TestEntityModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingItem = this.data.TestEntities.Find(id);

            if (existingItem == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "test entity"));
            }

            // Check if some of the fields in the new data are not null
            if (!string.IsNullOrEmpty(item.Name))
            {
                existingItem.Name = item.Name;
            }

            this.data.SaveChanges();

            item.Id = existingItem.Id;

            return this.Ok(item);
        }

        // Registered Users Only
        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var existingItem = this.data.TestEntities.Find(id);

            if (existingItem == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "test entity"));
            }

            this.data.TestEntities.Delete(existingItem);
            this.data.SaveChanges();

            return this.Ok();
        }
    }
}
