using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database
{
    public class ObjectContext : DbContext
    {
        public DbSet<Object> Objects { get; set; }

        public ObjectContext(DbContextOptions<ObjectContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("object");
        }
    }
}
