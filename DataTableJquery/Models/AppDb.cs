using Microsoft.EntityFrameworkCore;
using System;

namespace DataTableJquery.Models
{
	public class AppDb : DbContext
	{
		public AppDb(DbContextOptions<AppDb> db) : base(db)
		{

		}

		public DbSet<Customer> customers { get; set; }
	}
}
