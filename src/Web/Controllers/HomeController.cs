using System;
using System.Linq;
using Application.Contracts;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOperationService _operationService;

        private readonly ICustomerCoinStack _customerCoinStack;
        private readonly IProductGrid _productGrid;

        public HomeController(IOperationService operationService, ICustomerCoinStack customerCoinStack,
            IProductGrid productGrid)
        {
            _operationService = operationService;
            _customerCoinStack = customerCoinStack;
            _productGrid = productGrid;
        }

        public IActionResult Index(string message = "")
        {
            ViewData["Message"] = message;

            var availableAmount = FormatAmountInEuros(_customerCoinStack.GetTotal());
            var products = _productGrid.GetAllProducts();

            var model = new HomeViewModel(availableAmount, products);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCoin(int coinValue)
        {
            _customerCoinStack.Push(coinValue);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReturnCoins()
        {
            var coins = _operationService.ReturnCoins();
            var message = $"The inserted coins were returned:<br />";

            var groupedCoins = coins
                .GroupBy(x => x)
                .Select(x => new {FaceValue = x.Key, Quantity = x.Count()})
                .OrderBy(x => x.FaceValue);

            message = groupedCoins.Aggregate(message,
                (current, coin) =>
                    current + $"<br />{coin.Quantity} x {FormatAmountInEuros(coin.FaceValue)} coin(s)");

            return RedirectToAction("Index", new {Message = message});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetProduct(int productCode)
        {
            var message = string.Empty;

            try
            {
                var result = _operationService.ProcessSale(productCode);

                message = $"Thank you! Sold {result.Product.Name} for {FormatAmountInEuros(result.Product.Price)}<br />";

                if (result.Change.Length > 0)
                {
                    message += "<br />Change:<br >";
                    
                    var coins = result.Change;
                    
                    var groupedCoins = coins
                        .GroupBy(x => x)
                        .Select(x => new {FaceValue = x.Key, Quantity = x.Count()})
                        .OrderBy(x => x.FaceValue);
                    
                    message = groupedCoins.Aggregate(message,
                        (current, coin) =>
                            current + $"<br />{coin.Quantity} x {FormatAmountInEuros(coin.FaceValue)} coin(s)");
                }
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return RedirectToAction("Index", new {Message = message});
        }

        private static string FormatAmountInEuros(int amount) => amount == 0
            ? "€ 0"
            : $"€ {(decimal) amount / 100:0.00}";
    }
}