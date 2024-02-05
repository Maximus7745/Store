using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using System.Diagnostics;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 12;
        public HomeController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string category, int productPage = 1)
        => View(new ProductsListViewModel
        {
            Products = repository.Products
        .Where(p => category == null || p.Category == category)
        .OrderBy(p => p.Id)
        .Skip((productPage - 1) * PageSize)
        .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ProductsPerPage = PageSize,
                TotalProducts = category == null ?
                repository.Products.Count() :
                repository.Products.Where(e =>
                e.Category == category).Count()

            },
            CurrentCategory = category
        }) ;


        public ViewResult SelectProduct(int productId)
        {
            return View(repository.Products.Where(p => p.Id == productId).FirstOrDefault());
        }


    }

}