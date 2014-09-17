namespace MusicCatalog.Data.Repositories
{
    using System;
    using System.Collections.Generic;

    using MusicCatalog.Data.Interfaces;
    using MusicCatalog.Models;

    public class AppData : IAppData
    {
        private IAppDbContext context;
        private IDictionary<Type, object> repositories;

        ////public AppData()
        ////    : this (new MusicCatalogDbContext())
        ////{
        ////}

        // Using Ninject Dependency Injection
        public AppData(IAppDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Album> Albums
        {
            get
            {
                return this.GetRepository<Album>();
            }
        }

        public IRepository<Song> Songs
        {
            get
            {
                return this.GetRepository<Song>();
            }
        }

        public IRepository<Artist> Artists
        {
            get
            {
                return this.GetRepository<Artist>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(AppRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
