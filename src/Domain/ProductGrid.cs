using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Contracts;

namespace Domain
{
    public class ProductGrid : IProductGrid
    {
        private List<Product> Products { get; } = new List<Product>();

        public ProductGrid()
        {
            // Seeding TODO remove
            AddProduct(1, "Tea", 130, 10);
            AddProduct(2, "Espresso", 180, 20);
            AddProduct(3, "Juice", 180, 20);
            AddProduct(4, "Chicken soup", 180, 15);
            AddProduct(5, "Chai", 200, 1);
        }

        public Product AddProduct(int code, string name, int price, int quantity)
        {
            if (Products.Any(x => x.Code == code)) throw new ArgumentException("The specified code is already assigned");

            var product = new Product(code, name, price, quantity);
            Products.Add(product);
            
            return product;
        }

        public void Sell(int code)
        {
            var product = Products.SingleOrDefault(x => x.Code == code);
            
            if (product == default) throw new ArgumentNullException(nameof(code), "No product with the specified code was found");

            product.Sell();
        }
    }
}