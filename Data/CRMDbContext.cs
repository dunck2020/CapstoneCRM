using CapstoneCRM.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneCRM.Data
{
    public class CRMDbContext : DbContext
    {
        public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BusinessLeads> BusinessLeads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one to many relationship employee has many leads
            modelBuilder.Entity<BusinessLeads>()
                .HasOne(l => l.AssignedEmployee)
                .WithMany(e => e.BusinessLeads);

            // one to many relationship employee has many customers
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.AssignedEmployee)
                .WithMany(e => e.Customers);

            // one to many relationship customer type has many customers
            modelBuilder.Entity<Customer>()
                .HasOne(l => l.CustomerType)
                .WithMany(e => e.Customers);

            // one to many relationship department has many employees
            modelBuilder.Entity<Employee>()
                .HasOne(l => l.Department)
                .WithMany(e => e.Employees);

        }
    }
}
