using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManagement.Domain.Entities;

namespace TruckManagement.Infrastructure
{
    public class TruckManagementDbContext : DbContext
    {
        public TruckManagementDbContext() { }
        public TruckManagementDbContext(DbContextOptions<TruckManagementDbContext> options) : base(options) { }
        public DbSet<Truck> Trucks { get; set; }  


    }
}
