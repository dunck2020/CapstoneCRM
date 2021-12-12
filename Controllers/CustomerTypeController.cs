using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapstoneCRM.Data;
using CapstoneCRM.Models;
using Microsoft.AspNetCore.Authorization;

namespace CapstoneCRM.Controllers
{
    public class CustomerTypeController : Controller
    {
        private readonly CRMDbContext _context;

        public CustomerTypeController(CRMDbContext context)
        {
            _context = context;
        }

        // GET: CustomerType
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerTypes.ToListAsync());
        }

        // GET: CustomerType/Details/5
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        // GET: CustomerType/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerType);
        }

        // GET: CustomerType/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerTypes.FindAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }
            return View(customerType);
        }

        // POST: CustomerType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CustomerType customerType)
        {
            if (id != customerType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerTypeExists(customerType.Id))
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
            return View(customerType);
        }

        // GET: CustomerType/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        // POST: CustomerType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerType = await _context.CustomerTypes.FindAsync(id);
            _context.CustomerTypes.Remove(customerType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerTypeExists(int id)
        {
            return _context.CustomerTypes.Any(e => e.Id == id);
        }
    }
}
