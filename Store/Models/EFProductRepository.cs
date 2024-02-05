namespace Store.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext dbContext) {
        
            context = dbContext;
        }
        public IQueryable<Product> Products => context.Products;


    }
}
