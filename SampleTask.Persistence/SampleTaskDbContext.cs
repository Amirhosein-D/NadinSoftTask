using Microsoft.EntityFrameworkCore;
using SampleTask.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Persistence
{
    public class SampleTaskDbContext : DbContext
    {
        public SampleTaskDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SampleTaskDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
