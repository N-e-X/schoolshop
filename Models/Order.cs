using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class Order
    {
        public Order()
        {
            Number = GenerateNumber();
            Items = new List<OrderItem>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public List<OrderItem> Items { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        //public Discount Discount { get; set; }

        private string GenerateNumber()
        {
            return $"{DateTime.Now.ToString("yyyyMMdd-HHmmss")}";
        }

        //public override string ToString()
        //{
        //    var stringBuilder = new StringBuilder();

        //    stringBuilder.AppendLine($"Заказ №{Number}");
        //    stringBuilder.AppendLine($"Название | кол-во | стоимость |");

        //    foreach (var item in Items)
        //    {
        //        var productNameLength = item.Product.Name.Length;

        //        var productName = item.Product.Name;
        //        if (productNameLength < 8)
        //            productName += string.Join(string.Empty, Enumerable.Range(0, 8 - productNameLength).Select(x => " "));

        //        stringBuilder.AppendLine("-----------------------------");
        //        stringBuilder.AppendLine($"{productName} |   {item.Count}    |      {item.Product.Price}");
        //    }

        //    stringBuilder.AppendLine("-----------------------------");
        //    var totalSum = Items.Sum(x => x.Product.Price * x.Count);
        //    if (Discount != null)
        //    {
        //        totalSum = Discount.Apply(totalSum);
        //        stringBuilder.AppendLine($"Была применена скидка: {Discount}");
        //        stringBuilder.AppendLine($"Итоговая стоимость со учетом скидки: {totalSum}");
        //    }
        //    else
        //    {
        //        stringBuilder.AppendLine($"Итоговая стоимость: {totalSum}");
        //    }

        //    return stringBuilder.ToString();
        //}
    }
}