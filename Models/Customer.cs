using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Order> Orders { get; set; }

        public Customer()
        { }

        public Customer(string name, string phoneNumber = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Покупатель не может существовать без имени");

            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
