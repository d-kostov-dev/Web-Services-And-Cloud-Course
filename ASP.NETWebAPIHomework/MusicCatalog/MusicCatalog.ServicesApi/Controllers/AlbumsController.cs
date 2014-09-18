namespace MusicCatalog.ServicesApi.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using MusicCatalog.Data.Interfaces;
    using MusicCatalog.Data.Repositories;
    using MusicCatalog.Models;
    using MusicCatalog.ServicesApi.Models;

    public class AlbumsController : BaseApiController
    {
        ////public AlbumsController()
        ////    : this(new AppData())
        ////{
        ////}

        // Using Ninject Dependency Injection
        public AlbumsController(IAppData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var allItems = this.data.Albums
                .All()
                .Select(AlbumModel.PrepareModel);

            return this.Ok(allItems);
        }

        [HttpPost]
        public IHttpActionResult Create(AlbumModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newItem = new Album()
            {
                Title = item.Title,
                Year = item.Year,
                Producer = item.Producer,
            };

            this.data.Albums.Add(newItem);
            this.data.Albums.SaveChanges();

            item.Id = newItem.Id;

            return this.Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, AlbumModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var existingItem = this.data.Albums.Find(id);

            if (existingItem == null)
            {
                return this.BadRequest("Invalid Album");
            }

            // Check if some of the fields in the new data are not null
            if (!string.IsNullOrEmpty(item.Title))
            {
                existingItem.Title = item.Title;
            }

            if (!string.IsNullOrEmpty(item.Producer))
            {
                existingItem.Producer = item.Producer;
            }
            
            existingItem.Year = item.Year;

            this.data.Albums.SaveChanges();

            item.Id = existingItem.Id;

            return this.Ok(item);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingItem = this.data.Albums.Find(id);
            if (existingItem == null)
            {
                return this.BadRequest("Such Item does not exists!");
            }

            this.data.Albums.Delete(existingItem);
            this.data.SaveChanges();

            return this.Ok();
        }

        // /api/albums/AddArtist/INT?artistId=INT
        [HttpPost]
        public IHttpActionResult AddArtist(int id, int artistId)
        {
            var album = this.data.Albums.Find(id);
            if (album == null)
            {
                return BadRequest("Such album does not exists - invalid id!");
            }

            var artist = this.data.Artists.Find(artistId);
            if (artist == null)
            {
                return BadRequest("Such artist does not exists - invalid id!");
            }

            album.Artists.Add(artist);
            this.data.SaveChanges();

            return Ok();
        }

        // /api/albums/AddSong/INT?songId=INT
        [HttpPost]
        public IHttpActionResult AddSong(int id, int songId)
        {
            var album = this.data.Albums.Find(id);
            if (album == null)
            {
                return BadRequest("Such album does not exists - invalid id!");
            }

            var song = this.data.Songs.Find(songId);
            if (song == null)
            {
                return BadRequest("Such song does not exists - invalid id!");
            }

            album.Songs.Add(song);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
