﻿namespace Application.Data
{
    using System.Data.Entity;

    using Application.Data.Interfaces;
    using Application.Data.Migrations;
    using Application.Models;

    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class ApplicationDbContext : IdentityDbContext<User>, IDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public virtual IDbSet<TestEntity> TestEntities { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
