/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrodyagaWeb.Data;
using BrodyagaWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrodyagaWeb.Controllers
{
    public class UnitsController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public UnitsController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: Platoons
        public async Task<IActionResult> Index()
        {
            if(_context.Units == null)
                return Problem("Entity set 'BrodyagaWebContext.Platoon'  is null.");
            var vUnit = await _context.Units.
                OrderBy(t => t.OrderVal).
                ThenBy(t => t.ParentId).
                ThenBy(t => t.Number).ToListAsync();
            return View(vUnit);
        }

        // GET: Platoons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var vUnit = await _context.Units.
                Include(t => t.Parent).
                FirstOrDefaultAsync(m => m.Id == id);
            if (vUnit == null)
            {
                return NotFound();
            }

            return View(vUnit);
        }

        // GET: Platoons/Create
        public IActionResult Create()
        {
            ViewData["Units"] = new SelectList(_context.Units.OrderBy(t => t.OrderVal), "Id", "Number");
            return View();
        }

        // POST: Platoons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,ParentId,OrderVal")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                unit.Id = Guid.NewGuid();
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: Platoons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var vUnit = await _context.Units.FindAsync(id);
            if (vUnit == null)
            {
                return NotFound();
            }
            vUnit.Users = await _context.Users.
                Where(t => t.UnitId == id).
                OrderBy(t => t.FirstName).
                ToListAsync();
            ViewData["Units"] = new SelectList(_context.Units.OrderBy(t => t.OrderVal), "Id", "Number", vUnit.ParentId);
            return View(vUnit);
        }

        // POST: Platoons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Number,ParentId,OrderVal")] Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.Id))
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
            ViewData["Units"] = new SelectList(_context.Units.OrderBy(t => t.OrderVal), "Id", "Number", unit.ParentId);
            return View(unit);
        }

        // GET: Platoons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var platoon = await _context.Units
                .FirstOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            return View(platoon);
        }

        // POST: Platoons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Units == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.Platoon'  is null.");
            }
            var platoon = await _context.Units.FindAsync(id);
            if (platoon != null)
            {
                _context.Units.Remove(platoon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(Guid id)
        {
          return (_context.Units?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
