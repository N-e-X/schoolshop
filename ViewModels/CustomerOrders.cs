using System.Collections.Generic;
using Shop.Models;

namespace Shop.ViewModels
{
    public class CustomerOrders
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<string> OrderNumbers { get; set; }

        private CustomerOrders()
        { }

        public CustomerOrders(int customerId, string customerName, IEnumerable<string> orderNumbers)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            OrderNumbers = orderNumbers;
        }
    }
}