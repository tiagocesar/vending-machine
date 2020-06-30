using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Contracts;
using Domain.Exceptions;

namespace Domain
{
    public class ProductGrid : IProductGrid
    {
        private List<Product> Products { get; } = new List<Product>();

        public Product AddProduct(int code, string name, int price, int quantity)
        {
            if (Products.Any(x => x.Code == code))
                throw new ArgumentException("The specified code is already assigned");

            var product = new Product(code, name, price, quantity);
            Products.Add(product);

            return product;
        }

        public Product GetProduct(int code)
        {
            var product = Products.SingleOrDefault(x => x.Code == code);

            if (product == default)
                throw new ArgumentNullException(nameof(code), "No product with the specified code was found");
            if (product.Quantity == 0) throw new ProductNotAvailableException("The product is depleted");

            return product;
        }

        public IReadOnlyCollection<Product> GetAllProducts() => Products;
    }
}