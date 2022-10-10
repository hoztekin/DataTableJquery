using DataTableJquery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DataTableJquery.Controllers
{
	public class TableController : Controller
	{
		private readonly AppDb db;
		

		public TableController(AppDb db)
		{
			this.db = db;
		}

		public IActionResult Index()
		{
			var result = db.customers.ToList();
			return View(result);
		}

		public IActionResult GetCustomerList() 
		{
			
			return View();

		}
	}
}
