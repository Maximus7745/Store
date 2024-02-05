namespace Store.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
