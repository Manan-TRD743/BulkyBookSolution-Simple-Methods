using System.Linq.Expressions;

// Define a generic interface for a repository
public interface Irepository<T> where T : class
{
    // Get all elements of type T from the repository
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, String? includeProperties = null);

    // Get a single element of type T from the repository that matches the given filter expression
    T Get(Expression<Func<T, bool>> filter, String? includeProperties = null);

    // Add a new element of type T to the repository
    void Add(T item);

    // Remove an existing element of type T from the repository
        void Remove(T item);

    // Remove a range of elements of type T from the repository
        void RemoveRange(IEnumerable<T> item);
}