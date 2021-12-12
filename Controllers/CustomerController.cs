using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using CapstoneCRM.Data;
using CapstoneCRM.Models;

namespace CapstoneCRM.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CRMDbContext _context;

        public CustomerController(CRMDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CitySort"] = sortOrder == "City" ? "city_desc" : "City";
            ViewData["StateSort"] = sortOrder == "State" ? "state_desc" : "State";
            ViewData["ClassSort"] = sortOrder == "Class" ? "class_desc" : "Class";
            ViewData["PersonSort"] = sortOrder == "Person" ? "person_desc" : "Person";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var customers = from c in _context.Customers select c;

            //var customers = _context.Customers.Include(c => c.AssignedEmployee).Include(c => c.CustomerType);

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.BusinessName.Contains(searchString) ||
                                               s.City.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "City":
                    customers = customers.OrderBy(s => s.City);
                    break;
                case "city_desc":
                    customers = customers.OrderByDescending(s => s.City);
                    break;
                case "State":
                    customers = customers.OrderBy(s => s.State);
                    break;
                case "state_desc":
                    customers = customers.OrderByDescending(s => s.State);
                    break;
                case "Class":
                    customers = customers.OrderBy(s => s.CustomerType);
                    break;
                case "class_desc":
                    customers = customers.OrderByDescending(s => s.CustomerType);
                    break;
                case "Person":
                    customers = customers.OrderBy(s => s.AssignedEmployee);
                    break;
                case "person_desc":
                    customers = customers.OrderByDescending(s => s.AssignedEmployee);
                    break;
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.BusinessName);
                    break;
                //default name sort
                default:
                    customers = customers.OrderBy(s => s.BusinessName);
                    break;
            }

            int pageSize = 7;
            customers = customers.Include(c => c.AssignedEmployee).Include(c => c.CustomerType);

            return View(await PaginatedList<Customer>.CreateAsync(customers.AsNoTracking(), pageNumber ?? 1, pageSize));

            //var cRMDbContext = _context.Customers.Include(c => c.AssignedEmployee).Include(c => c.CustomerType);
            //return View(await cRMDbContext.ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.AssignedEmployee)
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            ViewData["CustomerTypeID"] = new SelectList(_context.CustomerTypes, "Id", "Id");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BusinessName,Address,City,State,Zip,BusPhone,PrimaryContactName,ContactPhone,ContactEmail,CustomerNotes,CustomerTypeID,EmployeeId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", customer.EmployeeId);
            ViewData["CustomerTypeID"] = new SelectList(_context.CustomerTypes, "Id", "Id", customer.CustomerTypeID);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", customer.EmployeeId);
            ViewData["CustomerTypeID"] = new SelectList(_context.CustomerTypes, "Id", "Id", customer.CustomerTypeID);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BusinessName,Address,City,State,Zip,BusPhone,PrimaryContactName,ContactPhone,ContactEmail,CustomerNotes,CustomerTypeID,EmployeeId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", customer.EmployeeId);
            ViewData["CustomerTypeID"] = new SelectList(_context.CustomerTypes, "Id", "Id", customer.CustomerTypeID);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.AssignedEmployee)
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
