using Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class DataBaseContext : DbContext
	{
		public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
	}
}