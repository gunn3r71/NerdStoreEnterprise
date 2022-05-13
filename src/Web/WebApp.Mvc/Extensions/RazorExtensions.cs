using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NerdStoreEnterprise.WebApp.Mvc.Extensions
{
    public static class RazorExtensions
    {
        public static string StockMessage(this RazorPage page, int amount)
        {
            return amount > 0 ? $"Only {amount} in stock!" : "Sold out product";
        }

        public static string CurrencyFormatter(this RazorPage page, decimal value)
        {
            return value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Free";
        }

        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            var hasher = MD5.Create();
            var data = hasher.ComputeHash(Encoding.Default.GetBytes(email));
            var strBuilder = new StringBuilder();

            data.ToList().ForEach(x => strBuilder.Append(x.ToString("x2")));

            return strBuilder.ToString();
        }
    }
}