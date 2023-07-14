using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Entity;
using System.Data.Entity.Infrastructure;
using WEB.Controllers;

namespace WEB.Models
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        //public DbSet<HomeController> HomeControllers { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<BodyType> BodyType { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarType> CarType { get; set; }
        public DbSet<Drive_type> Drive_type { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Transmission> Transmission { get; set; }
        public DbSet<WEB.Entity.Reservation> Reservation { get; set; } = default!;
        public DbQuery<Cars> Cars { get; set; }

        public DbSet<CarOrder> CarOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>().ToView("CarView");
            modelBuilder.Entity<CarOrder>().ToView("Car_Order");
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
