using Microsoft.AspNetCore.Mvc;
using easy_farmers.Models;
using easy_farmers.Helpers; // Ensure this namespace exists
using System.Collections.Generic;

namespace easy_farmers.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey);
            return View(cart ?? new List<CartItem>());
        }

        public IActionResult AddToCart(int id, string name, decimal price)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
            var item = cart.Find(p => p.ProductId == id);

            if (item == null)
                cart.Add(new CartItem { ProductId = id, ProductName = name, Price = price, Quantity = 1 });
            else
                item.Quantity++;

            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
            cart.RemoveAll(p => p.ProductId == id);
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            return RedirectToAction("Index");
        }
    }
}
