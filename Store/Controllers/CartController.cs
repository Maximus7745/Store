using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Store.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        [Authorize]
        public ViewResult Index(string returnUrl)
        {
            return base.View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        
        }
        [Authorize]
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [Authorize]
        public RedirectToActionResult RemoveFromCart(int productld,string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.Id == productld);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
return RedirectToAction("Index", new { returnUrl });
            }
    private Cart GetCart()
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
    return cart;
    }
    private void SaveCart(Cart cart)
    {
        HttpContext.Session.SetJson("Cart", cart);
    }
}
}