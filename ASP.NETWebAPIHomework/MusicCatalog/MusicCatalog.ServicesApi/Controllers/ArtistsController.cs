namespace MusicCatalog.ServicesApi.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using MusicCatalog.Data.Interfaces;
    using MusicCatalog.Models;
    using MusicCatalog.ServicesApi.Models;

    public class ArtistsController : BaseApiController
    {
        // Using Ninject Dependency Injection
        public ArtistsController(IAppData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var allItems = this.data.Artists
                .All()
                .Select(ArtistModel.PrepareModel);

            return this.Ok(allItems);
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newItem = new Artist()
            {
                Name = item.Name,
                Country = item.Country,
                DateOfBirth = item.DateOfBirth,
            };

            this.data.Artists.Add(newItem);
            this.data.Artists.SaveChanges();

            item.Id = newItem.Id;

            return this.Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ArtistModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingItem = this.data.Artists.Find(id);

            if (existingItem == null)
            {
                return this.BadRequest("Invalid Course");
            }

            // Check if some of the fields in the new data are not null
            if (!string.IsNullOrEmpty(item.Name))
            {
                existingItem.Name = item.Name;
            }

            if (!string.IsNullOrEmpty(item.Country))
            {
                existingItem.Country = item.Country;
            }

            if (item.DateOfBirth != null)
            {
                existingItem.DateOfBirth = item.DateOfBirth;
            }

            this.data.Artists.SaveChanges();

            item.Id = existingItem.Id;

            return this.Ok(item);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingItem = this.data.Artists.Find(id);
            if (existingItem == null)
            {
                return this.BadRequest("Such Item does not exists!");
            }

            this.data.Artists.Delete(existingItem);
            this.data.SaveChanges();

            return this.Ok();
        }
    }
}
