using Microsoft.EntityFrameworkCore;
using System.Globalization;
using CsvHelper;

namespace Store.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new List<Product>();
            using (var reader = new StreamReader("Data\\products.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var product = new Product
                        {
                            Id = csv.GetField<int>("id"),
                            Name = csv.GetField<string>("name"),
                            Price = decimal.Parse(csv.GetField<string>("price").Trim().Replace(".", ""), 
                            NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands),
                            Rating = csv.GetField<double>("rating"),
                            ImageUrl = csv.GetField<string>("image_url"),
                            Shop = csv.GetField<string>("shop"),
                            Category = csv.GetField<string>("category"),
                        };
                        products.Add(product);
                    }
                }
            }
            modelBuilder.Entity<Product>().HasData(
                    products
            );
        }
    }
}
