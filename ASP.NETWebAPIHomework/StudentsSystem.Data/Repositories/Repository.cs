namespace StudentsSystem.Data.Repositories
{
    using System.Data.Entity;

    using StudentsSystem.Data.Interfaces;

    public class Repository<T> : IRepository<T> where T : class
    {
        private IDbContext context;
        private IDbSet<T> set;

        public Repository()
            : this(new StudentSystemContext()) // Next use change only this.
        {
        }

        public Repository(IDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public System.Linq.IQueryable<T> All()
        {
            return this.set;
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
