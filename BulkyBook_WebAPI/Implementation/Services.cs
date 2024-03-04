/*using BulkyBook_WebAPI.Data;
using BulkyBook_WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook_WebAPI.Implementation
{
    public class Services<T> : IServices<T> where T : class
    {

        private ApplicationDbContext DbContext;
        internal DbSet<T> Dbset;

        public Services(ApplicationDbContext dbContext)
        { 
            DbContext = dbContext;
            this.Dbset = DbContext.Set<T>();

        }
        public void Add(T item)
        {
            Dbset.Add(item);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = Dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = Dbset;
            return query.ToList();
        }

        public void Remove(T item)
        {
           Dbset.Remove(item);
        }
    }
}*/