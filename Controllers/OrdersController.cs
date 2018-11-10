using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Controllers
{
    [Route("orders")]
    public class OrdersController : Controller
    {

        public ShopContext _db;

        public OrdersController(ShopContext db)
        {
            _db = db;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var orders = await _db.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ToListAsync();

            var ordersViewModel = orders
                .Select(order => new OrderViewModel
                {
                    Number = order.Number,
                    Items = order.Items
                        .Select(item => new OrderItemViewModel
                        {
                            ProductName = item.Product.Name,
                            ProductPrice = item.Product.Price,
                            ProductCount = item.Count
                        }).ToList()
                });

            return View(ordersViewModel);
        }

        [Route("add")]
        public async Task<IActionResult> Add()
        {
            List<OrderItemViewModel> productList = await _db.Products
                .Select(x => new OrderItemViewModel
                {
                    ProductId = x.Id,
                    ProductName = x.Name,
                    ProductPrice = x.Price
                })
                .ToListAsync();

            return View(productList);
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add(List<OrderItemViewModel> orderItemViewModels)
        {

            if (ModelState.IsValid)
            {
                var order = new Order();
                await _db.Orders.AddAsync(order);
                foreach (var itemViewModel in orderItemViewModels)
                {
                    if (itemViewModel.ProductCount != 0)
                    {
                        await _db.OrderItems.AddAsync(new OrderItem
                        {
                            ProductId = itemViewModel.ProductId,
                            Count = itemViewModel.ProductCount,
                            OrderId = _db.Orders.Last().Id
                        });
                    }
                }

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            return View();
        }
    }
}