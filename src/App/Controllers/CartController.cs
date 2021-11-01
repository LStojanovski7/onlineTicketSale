using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Entities;

namespace App.Controllers
{
   public class CartController : Controller
   {
       private readonly UserManager<ApplicationUser> _userManager;
       private readonly ICartServices _cartServices;
       private readonly ITicketServices _ticketServices;

       public CartController(UserManager<ApplicationUser> userManager
                    ,ICartServices cartServices
                    ,ITicketServices ticketServices)
       {
           _userManager = userManager;
           _cartServices = cartServices;
           _ticketServices = ticketServices;
       }
       // GET: CartController
       [Authorize(Roles = "admin,standard" )]
       public ActionResult Index()
       {
           List<Tickets> tickets = _cartServices.GetCart();

           if(tickets != null)
           {
               return View(tickets);
           }

           return RedirectToAction("Index", "Home", new { area = "" });
       }

       // GET: AddToCart
       [Authorize(Roles = "admin,standard")]
       public ActionResult AddToCart(int? id)
       {
           Tickets ticket = _ticketServices.GetTicketDetails(id);

           if(id != null)
           {
               _cartServices.AddToCart(ticket);
           }
           else 
           {
               throw new TicketNotFoundException("No such ticket: id");
           }

           return RedirectToAction("Index");
       }

       [Authorize(Roles = "admin,standard")]
       public ActionResult Remove(int? id)
       {
           if(id == null)
           {
               throw new TicketNotFoundException("No such ticket: id");
           }

           Tickets ticket = _ticketServices.GetTicketDetails(id);

            _cartServices.Remove(ticket);

           return RedirectToAction("Index");
       }

       [Authorize(Roles = "admin,standard")]
       public ActionResult Checkout()
       {
           return View("Checkout");
       }

   }
}
