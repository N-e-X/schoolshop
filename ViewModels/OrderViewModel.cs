using System.Collections.Generic;
using System.Linq;
using Shop.Models;

namespace Shop.ViewModels
{
    public class OrderViewModel
    {
        public string Number { get; set; }

        public decimal TotalSum => Items.Sum(x => x.TotalSum);

        public List<OrderItemViewModel> Items { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public decimal TotalSum => ProductPrice * ProductCount;
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public decimal ProductPrice { get; set; }
    }
}

