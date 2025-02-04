using System;
using Microsoft.EntityFrameworkCore;
using SalonApp2.Models

namespace SalonApp2.DBOperations
{

	public class CategoryDBContext	:DbContext
	{ 

        public CategoryDBContext(DbContextOptions<CategoryDBContext> options) :base(options)
		{

		}

		public DbSet<Category> Categories { get; set; }
	}
}
