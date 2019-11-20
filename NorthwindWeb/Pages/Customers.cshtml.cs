using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindContextLib;

namespace NorthwindWeb.Pages
{
    public class CustomersModel : PageModel
    {
        public IGrouping<string, CustomerData>[] CustomersPerCountry { get; set; }

        public Northwind db;
        public CustomersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Customers";
            var customers = db.Customers.OrderBy(c => c.ContactName).Select(c => new CustomerData { ID = c.CustomerID, Name = c.CompanyName, Country = c.Country }).ToArray();
            CustomersPerCountry = customers.GroupBy(c => c.Country).OrderBy(g => g.Key).ToArray();
        }

        public struct CustomerData
        {
            public string ID;
            public string Country;
            public string Name;
        }
    }
}