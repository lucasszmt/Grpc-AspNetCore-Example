using EcoGrpc.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace EcoSpider.Data
{
    public class DataContext : DbContext
    {
        public  DbSet<CustomerModel> Customers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}