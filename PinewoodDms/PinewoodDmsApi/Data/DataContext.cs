using Microsoft.EntityFrameworkCore;
using PinewoodDmsApi.Models;

namespace PinewoodDmsApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}
