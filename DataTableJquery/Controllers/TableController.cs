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
			
			return View();
		}

		public IActionResult GetCustomerList()
		{
			var result = db.customers.ToList();
			return new JsonResult(result);
		}
	}
}
