using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database
{
    public class TourContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }

        public TourContext(DbContextOptions<TourContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tour");
        }
    }
}
