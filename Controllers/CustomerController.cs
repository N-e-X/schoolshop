using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.ViewModels;

namespace Shop.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private readonly ShopContext _context;

        public CustomerController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id:int}/orders")]
        public async Task<IActionResult> Orders(int id)
        {
            var customer = await _context.Customers
                .Select(x => new {x.Id, x.Name})
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                return NotFound();

            var orderNumbers = await _context.Orders
                .Select(x => new {x.CustomerId, x.Number})
                .Where(x => x.CustomerId == customer.Id)
                .Select(x => x.Number)
                .ToListAsync();

            var ordersInfo = new CustomerOrdersViewModel(
                customer.Id,
                customer.Name,
                orderNumbers);

            return View(ordersInfo);
        }
    }
}