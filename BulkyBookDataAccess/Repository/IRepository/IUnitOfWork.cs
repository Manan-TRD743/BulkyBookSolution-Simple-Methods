namespace BulkyBookDataAccess.Repository.IRepository
{
    // Interface for Unit of Work pattern, which manages multiple repositories
    public interface IUnitOfWork
    {
        // Accessor for Category repository
        ICategoryRepository Category { get; }

        // Accessor for Product repository
        IProductRepository Product { get; }

        // Accessor for Company repository
        ICompanyRepository Company { get; }

        // Accessor for Shopping Cart repository
        IShoppingCartRepository ShoppingCart { get; }

        // Accessor for ApplicationUser repository
        IApplicationUserRepository ApplicationUser { get; }

        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; } 

        // Saves changes made in repositories
        void Save();
    }
}

