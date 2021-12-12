using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapstoneCRM.Data;
using CapstoneCRM.Models;

namespace CapstoneCRM.Controllers
{
    public class BusinessLeadController : Controller
    {
        private readonly CRMDbContext _context;

        public BusinessLeadController(CRMDbContext context)
        {
            _context = context;
        }

        // GET: BusinessLead
        public async Task<IActionResult> Index()
        {
            var cRMDbContext = _context.BusinessLeads.Include(b => b.AssignedEmployee);
            return View(await cRMDbContext.ToListAsync());
        }

        // GET: BusinessLead/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessLeads = await _context.BusinessLeads
                .Include(b => b.AssignedEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessLeads == null)
            {
                return NotFound();
            }

            return View(businessLeads);
        }

        // GET: BusinessLead/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            return View();
        }

        // POST: BusinessLead/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BusinessName,Address,City,State,Contact,Phone,EmployeeId")] BusinessLeads businessLeads)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessLeads);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", businessLeads.EmployeeId);
            return View(businessLeads);
        }

        // GET: BusinessLead/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessLeads = await _context.BusinessLeads.FindAsync(id);
            if (businessLeads == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", businessLeads.EmployeeId);
            return View(businessLeads);
        }

        // POST: BusinessLead/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BusinessName,Address,City,State,Contact,Phone,EmployeeId")] BusinessLeads businessLeads)
        {
            if (id != businessLeads.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessLeads);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessLeadsExists(businessLeads.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", businessLeads.EmployeeId);
            return View(businessLeads);
        }

        // GET: BusinessLead/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessLeads = await _context.BusinessLeads
                .Include(b => b.AssignedEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (businessLeads == null)
            {
                return NotFound();
            }

            return View(businessLeads);
        }

        // POST: BusinessLead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessLeads = await _context.BusinessLeads.FindAsync(id);
            _context.BusinessLeads.Remove(businessLeads);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessLeadsExists(int id)
        {
            return _context.BusinessLeads.Any(e => e.Id == id);
        }
    }
}
