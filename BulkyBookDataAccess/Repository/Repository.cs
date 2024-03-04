using BulkyBookSolution.BulkyBookDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBookDataAccess.Repository
{
    public class Repository<T> : Irepository<T> where T : class
    {
        private ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        
        public Repository(ApplicationDbContext context)
        {
            _db = context;
            this.dbSet = _db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u => u.CategoryID);
        }

        #region Add Item
        public void Add(T item)
        {
            dbSet.Add(item);
        }
        #endregion

        #region GetItem
        // Define a generic method called Get that returns a single element of type T that matches the given filter expression
        public T Get(Expression<Func<T, bool>> filter, String? includeProperties = null)
        {
            // Start by creating a query that returns all elements of type T from the database
            IQueryable<T> query = dbSet;
            
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Include the specified related entity in the query results
                    query = query.Include(property);
                }
            }
            // Apply the filter expression to the query
            query = query.Where(filter);

            // Return the first element that matches the filter expression, or null if no such element exists
            return query.FirstOrDefault();
        }
        #endregion

        #region Get All Item
        //Category Covertype
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, String? includeProperties = null)
        {
            // Start by creating a query that returns all elements of type T from the database
            IQueryable<T> query = dbSet;
            if(filter!= null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.
                    Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    // Include the specified related entity in the query results
                    query = query.Include(property);
                }
            }
            //Return Item List
            return query.ToList();
        }
        #endregion

        #region Remove Item
        public void Remove(T item)
        {
            dbSet.Remove(item);
        }
        #endregion

        #region Remove Range of item
        public void RemoveRange(IEnumerable<T> item)
        {
           dbSet.RemoveRange(item); 
        }
        #endregion

}
}
