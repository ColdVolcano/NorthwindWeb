using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthwindContextLib;
using NorthwindEntitiesLib;

namespace NorthwindWeb.Pages
{
    public class CustomerModel : PageModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public Northwind db;

        public CustomerModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public void OnGet()
        {
            if (Request.Query.ContainsKey("ID"))
            {
                Customer = db.Customers.Include(c => c.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(od => od.Product).FirstOrDefault(c => c.CustomerID == Request.Query["ID"].ToString());
            }
            else
                Customer = null;

            ViewData["Title"] = $"Northwind Web Site - {Customer?.CompanyName ?? "Error"}";
        }
    }
}