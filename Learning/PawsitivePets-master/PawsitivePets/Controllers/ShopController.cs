using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PawsitivePets.Data;
using PawsitivePets.Models;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawsitivePets.Controllers
{
    public class ShopController : Controller
    {
        // db connection instance
        private readonly ApplicationDbContext _context;

        // config instance to read vals from appsettings.json - needed to access Stripe payment key
        private IConfiguration _iconfiguration;

        // constructor - every instance of this class requires a DbContext object & an configuration object too!
        public ShopController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context; // make db connection public in this class so we can use it in any method
            _iconfiguration = configuration;
        }

        public IActionResult Index()
        {
            // pass a list of Categories to the view so the user can select one
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        // GET: Shop/ShopByCategory/5
        public IActionResult ShopByCategory(int id)
        {
            var pets = _context.Pets
                .Where(p => p.CategoryId == id)
                .OrderBy(p => p.Name)
                .ToList();

            // get category name for page heading
            var category = _context.Categories.Find(id);
            ViewBag.Category = category.Name;

            return View(pets);
        }

        // POST: /Shop/AddToCart
        [HttpPost]
        public IActionResult AddToCart(int PetId)
        {
            // get the pet's price
            var pet = _context.Pets.Find(PetId);
            var price = pet.Price;

            // determine who the user is: each user needs their own cart
            var customerId = GetCustomerId();

            // save to CartItems table in db
            var cartItem = new CartItem
            {
                PetId = PetId,
                Quantity = 1,
                Price = price,
                CustomerId = customerId
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            // redirect to the Cart page so user can see their full cart
            return RedirectToAction("Cart");
        }

        private string GetCustomerId()
        {
            // scenario 1: user already has items in their cart, so just use existing session var
            if (HttpContext.Session.GetString("CustomerId") != null)
            {
                return HttpContext.Session.GetString("CustomerId");
            }
            else
            {
                // scenario 2: user has no existing cart, but they are logged in
                // set user's email as the session var
                if (User.Identity.IsAuthenticated)
                {
                    HttpContext.Session.SetString("CustomerId", User.Identity.Name);
                }
                else
                {
                    // scenario 3: user has no existing cart and is not logged in
                    // create new unique value to write to session var
                    HttpContext.Session.SetString("CustomerId", Guid.NewGuid().ToString());
                }
                return HttpContext.Session.GetString("CustomerId");
            }
        }

        // GET: /Shop/Cart
        public IActionResult Cart()
        {
            // determine the CustomerId of this cart
            var customerId = GetCustomerId();

            // fetch the items in this cart from the db & include the Pet parent objects
            var cartItems = _context.CartItems
                .Include(c => c.Pet)
                .Where(c => c.CustomerId == customerId).ToList();

            // display the cart items in a view in the browser
            return View(cartItems);
        }

        // GET: /Shop/RemoveFromCart/5
        public IActionResult RemoveFromCart(int id)
        {
            // get object to be deleted
            var cartItem = _context.CartItems.Find(id);

            // delete from CartItems table
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            // refresh cart page
            return RedirectToAction("Cart");
        }

        // GET: /Shop/Checkout
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: /Shop/Checkout
        [Authorize]
        [HttpPost]
        public IActionResult Checkout([Bind("FirstName,LastName,Address,City,Province,PostalCode,Phone")] Models.Order order)
        {
            // auto-fill the other properties of the order
            order.OrderDate = DateTime.Now;
            order.CustomerId = User.Identity.Name;
            order.OrderTotal = (from c in _context.CartItems
                                where c.CustomerId == order.CustomerId
                                select c.Price).Sum();

            // save the order to a session var & start payment process
            HttpContext.Session.SetObject("Order", order);
            return RedirectToAction("Payment");
        }

        // GET: /Shop/Payment
        [Authorize]
        public IActionResult Payment()
        {
            return View();
        }

        // POST: /Shop/CreateCheckoutSession
        [Authorize]
        [HttpPost]
        public IActionResult CreateCheckoutSession()
        {
            // get order from session var
            var order = HttpContext.Session.GetObject<Models.Order>("Order");

            // set Stripe Secret Key val using config object which can read from appsettings.json
            //StripeConfiguration.ApiKey = _iconfiguration.GetSection("Stripe")["SecretKey"];
            StripeConfiguration.ApiKey = _iconfiguration.GetValue<string>("StripeSecretKey");

            // code copied from BB, modified from Stripe .NET docs
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = ((long?)(order.OrderTotal * 100)),
                        Currency = "cad",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Pawsitive Pets Purchase"
                        },
                    },
                    Quantity = 1
                  },
                },
                PaymentMethodTypes = new List<string>
                {
                  "card"
                },
                Mode = "payment",
                SuccessUrl = "https://" + Request.Host + "/Shop/SaveOrder",
                CancelUrl = "https://" + Request.Host + "/Shop/Cart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        // GET: /Shop/SaveOrder
        [Authorize]
        public IActionResult SaveOrder()
        {
            // save Order
            var order = HttpContext.Session.GetObject<Models.Order>("Order");
            _context.Orders.Add(order);
            _context.SaveChanges();

            // save Order Details
            var cartItems = _context.CartItems.Where(c => c.CustomerId == order.CustomerId);
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    PetId = item.PetId,
                    Price = item.Price,
                    OrderId = order.OrderId
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges(); // save all details to db in 1 transaction

            // empty cart
            foreach (var item in cartItems)
            {
                _context.CartItems.Remove(item);
            }
            _context.SaveChanges(); // commit transaction to db

            // show Order Details page as receipt/confirmation: /Orders/Details/5
            return RedirectToAction("Details", "Orders", new { @id = order.OrderId });
        }
    }
}
