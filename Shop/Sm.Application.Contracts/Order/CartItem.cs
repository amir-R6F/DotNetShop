using System;
using System.Text.RegularExpressions;

namespace Sm.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public double TotalItemsPrice { get; set; }
        
        public bool InStock { get; set; }

        public void CalculateTotal()
        {
            var price = Regex.Replace(UnitPrice, @"\D", "");
            TotalItemsPrice = double.Parse(price) * Count;
        }
    }
}