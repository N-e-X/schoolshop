using System.Collections.Generic;
using Shop.Models;

namespace Shop.ViewModels
{
    public class CustomerOrdersViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<string> OrderNumbers { get; set; }

        private CustomerOrdersViewModel()
        { }

        public CustomerOrdersViewModel(int customerId, string customerName, IEnumerable<string> orderNumbers)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            OrderNumbers = orderNumbers;
        }
    }
}