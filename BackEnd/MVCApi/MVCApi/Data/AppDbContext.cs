using Microsoft.EntityFrameworkCore;
using MVCApi.Models;
using System.Collections.Generic;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
