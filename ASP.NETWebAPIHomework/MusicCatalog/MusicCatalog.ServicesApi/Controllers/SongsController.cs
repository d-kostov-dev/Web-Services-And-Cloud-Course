namespace MusicCatalog.ServicesApi.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using MusicCatalog.Data.Interfaces;
    using MusicCatalog.Models;
    using MusicCatalog.ServicesApi.Models;

    public class SongsController : BaseApiController
    {
        // Using Ninject Dependency Injection
        public SongsController(IAppData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var allItems = this.data.Songs
                .All()
                .Select(SongModel.PrepareModel);

            return this.Ok(allItems);
        }

        [HttpPost]
        public IHttpActionResult Create(SongModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newItem = new Song()
            {
                Title = item.Title,
                Year = item.Year,
                Genre = item.Genre,
            };

            this.data.Songs.Add(newItem);
            this.data.Songs.SaveChanges();

            item.Id = newItem.Id;

            return this.Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SongModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingItem = this.data.Songs.Find(id);

            if (existingItem == null)
            {
                return this.BadRequest("Invalid Course");
            }

            // Check if some of the fields in the new data are not null
            if (!string.IsNullOrEmpty(item.Title))
            {
                existingItem.Title = item.Title;
            }

            if (!string.IsNullOrEmpty(item.Genre))
            {
                existingItem.Genre = item.Genre;
            }

            existingItem.Year = item.Year;

            this.data.Songs.SaveChanges();

            item.Id = existingItem.Id;

            return this.Ok(item);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingItem = this.data.Songs.Find(id);
            if (existingItem == null)
            {
                return this.BadRequest("Such Item does not exists!");
            }

            this.data.Songs.Delete(existingItem);
            this.data.SaveChanges();

            return this.Ok();
        }
    }
}
