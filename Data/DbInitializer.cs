using System.Linq;
using CapstoneCRM.Models;

namespace CapstoneCRM.Data
{
    public class DbInitializer
    {
        public static void Initialize(CRMDbContext context)
        {
            // determine if database has been seeded
            if (context.Employees.Any())
            {
                return;
            }

            // seed database with department types
            var dept = new Department[]
            {
                new Department { Name = "Director"},    // Id = 1
                new Department { Name = "Manager"},     // Id = 2
                new Department { Name = "Senior Level"},// Id = 3
                new Department { Name = "Mid Level"},   // Id = 4
                new Department { Name = "Entry Level"}, // Id = 5
                new Department { Name = "Internship"}   // Id = 6
            };

            foreach (Department d in dept)
            {
                context.Departments.Add(d);
            }

            // seed database with customer types
            var type = new CustomerType[]
            {
                new CustomerType {Name = "Elite" },    // Id = 1
                new CustomerType {Name = "Platinum" }, // Id = 2
                new CustomerType {Name = "Gold" },     // Id = 3
                new CustomerType {Name = "Silver" },   // Id = 4
                new CustomerType {Name = "Bronze" }    // Id = 5
            };

            foreach (CustomerType ct in type)
            {
                context.CustomerTypes.Add(ct);
            }

            // employee seeds
            var emp = new Employee[]
            {
                new Employee { Id = 1001,  Name = "Jack Reacher", DepartmentId = 1},
                new Employee { Id = 1002,  Name = "Olivia Smith", DepartmentId = 3},
                new Employee { Id = 1003,  Name = "Ava Dunham", DepartmentId = 2},
                new Employee { Id = 1004,  Name = "Charlotte Douglas", DepartmentId = 3},
                new Employee { Id = 1005,  Name = "Mac Williams", DepartmentId = 6},
                new Employee { Id = 1006,  Name = "Mia Stone", DepartmentId = 5}
            };
            foreach (Employee e in emp)
            {
                context.Employees.Add(e);
            }

            // customer seeds
            var cust = new Customer[]
            {
                new Customer
                {
                    BusinessName = "Turn2Media",
                    Address = "3435 Beitner Rd",
                    City = "Traverse City",
                    State = "MI",
                    Zip = "49684",
                    BusPhone = "231-555-6454",
                    PrimaryContactName = "Kevin Ramone",
                    ContactPhone = "231-555-8874",
                    ContactEmail = "kr@hotmail.com",
                    CustomerNotes = "Growing marketing business",
                    CustomerTypeID = 4,
                    EmployeeId = 1004
                },

                new Customer
                {
                    BusinessName = "BAY-45",
                    Address = "520 US 31 South",
                    City = "Traverse City",
                    State = "MI",
                    Zip = "49686",
                    BusPhone = "231-555-0045",
                    PrimaryContactName = "Charles Benson",
                    ContactPhone = "517-888-4545",
                    ContactEmail = "cbenson@hotmail.com",
                    CustomerTypeID = 2,
                    EmployeeId = 1004
                },
                new Customer
                {
                    BusinessName = "PRB Construction",
                    Address = "4191 Geranium",
                    City = "Traverse City",
                    State = "MI",
                    Zip = "49685",
                    BusPhone = "231-555-8787",
                    PrimaryContactName = "Paul BonCzyk",
                    ContactPhone = "231-555-8484",
                    ContactEmail = "paulie@hotmail.com",
                    CustomerTypeID = 3,
                    EmployeeId = 1002
                }
            };
            foreach (Customer c in cust)
            {
                context.Customers.Add(c);
            }

            // business lead seeds

            var lead = new BusinessLeads[]
            {
                new BusinessLeads
                {
                    BusinessName = "Jack's Taxidermy Service",
                    Address = "4323 Montgomery Ct",
                    City = "Fife Lake",
                    State = "MI",
                    Contact = "Jack Bishop",
                    Phone = "248-555-7878",
                    EmployeeId = 1001
                },
                new BusinessLeads
                {
                    BusinessName = "Janice's Flower Shop",
                    Address = "4151 Main St",
                    City = "Boyne City",
                    State = "MI",
                    Contact = "Karen Montgomery",
                    Phone = "517-555-8989",
                    EmployeeId = 1003
                }
            };
            foreach (BusinessLeads bl in lead)
            {
                context.BusinessLeads.Add(bl);
            }

            context.SaveChanges();

        }
    }
}
