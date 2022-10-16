using DataTableJquery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace DataTableJquery.Controllers
{
	public class ExcelController : Controller
	{
		private readonly IConfiguration configuration;
		private readonly AppDb db;

		public ExcelController(IConfiguration configuration, AppDb db)
		{
			this.configuration = configuration;
			this.db = db;
		}

		public IActionResult Index()
		{
			var result = db.customers.ToList();
			return View(result);
		}

		public IActionResult ImportExcelFile()
		{

			return View();

		}

		[HttpPost]
		public IActionResult ImportExcelFile(IFormFile formFile)
		{
			try
			{
				var mainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadExcelFile");

				if (!Directory.Exists(mainPath))
				{
					Directory.CreateDirectory(mainPath);
				}

				var filePath = Path.Combine(mainPath, formFile.FileName);
				using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
				{
					formFile.CopyTo(stream);
					stream.Flush();
					stream.Close();
					stream.Dispose();
				}
				var fileName = formFile.FileName;
				string extension = Path.GetExtension(fileName);
				string conString = string.Empty;

				switch (extension)
				{
					case ".xls":
						break;
						conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filePath + ";Extended Properties='Excel 8.0; HDR=Yes'";
						break;

					case ".xlsx":
						conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes'";
						break;

					default:
						break;
				}
				DataTable dt = new DataTable();
				conString = string.Format(conString, filePath);

				using (OleDbConnection conExcel = new OleDbConnection(conString))
				{
					using (OleDbCommand cmdExcel = new OleDbCommand())
					{
						using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
						{
							cmdExcel.Connection = conExcel;
							conExcel.Open();
							DataTable dtExcelSchema = conExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
							string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
							cmdExcel.CommandText = "SELECT * FROM [" + sheetName + "]";
							odaExcel.SelectCommand = cmdExcel;
							odaExcel.Fill(dt);
							conExcel.Close();

						}
					}
				}
				conString = configuration.GetConnectionString("Default");
				using (SqlConnection con = new SqlConnection(conString))
				{
					using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
					{
						sqlBulkCopy.DestinationTableName = "customers";
						sqlBulkCopy.ColumnMappings.Add("firstname", "firstname");
						sqlBulkCopy.ColumnMappings.Add("lastname", "lastname");
						sqlBulkCopy.ColumnMappings.Add("gender", "gender");
						sqlBulkCopy.ColumnMappings.Add("country", "country");
						sqlBulkCopy.ColumnMappings.Add("age", "age");
						con.Open();
						sqlBulkCopy.WriteToServer(dt);
						con.Close();
					}
				}
				TempData["message"]  = "DataSaved";
				return RedirectToAction("Index");
			}
			catch (System.Exception ex)
			{
				string msj = ex.Message;
			}
			return View();

		}

	}
}
