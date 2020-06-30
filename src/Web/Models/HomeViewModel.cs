using System.Collections.Generic;
using Domain;

namespace Web.Models
{
    public class HomeViewModel
    {
        public string AvailableAmount { get; }
        public IReadOnlyCollection<Product> Products { get; }

        public HomeViewModel(string availableAmount, IReadOnlyCollection<Product> products)
        {
            AvailableAmount = availableAmount;
            Products = products;
        }
    }
}