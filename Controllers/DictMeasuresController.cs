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

namespace BrodyagaWeb.Views
{
    public class DictMeasuresController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public DictMeasuresController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: Measures
        public async Task<IActionResult> Index()
        {
              return _context.DictMeasures != null ? 
                View(await _context.DictMeasures.OrderBy(t => t.Value).ToListAsync()) :
                Problem("Entity set 'BrodyagaWebContext.Measures'  is null.");
        }

        // GET: Measures/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (_context.DictMeasures == null)
            {
                return NotFound();
            }

            var measure = await _context.DictMeasures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View(measure);
        }

        // GET: Measures/Create
        public IActionResult Create()
        {
            Console.WriteLine("Create()");
            return View();
        }

        // POST: Measures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,Id,Comment")] DictMeasure measure)
        {
            Console.WriteLine($"MeasureCreate(Id:{measure.Id}, Value:{measure.Value}, Comment:{measure.Comment})");
            if (ModelState.IsValid)
            {
                measure.Id = Guid.NewGuid();
                _context.Add(measure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(measure);
        }

        // GET: Measures/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DictMeasures == null)
            {
                return NotFound();
            }

            var measure = await _context.DictMeasures.FindAsync(id);
            if (measure == null)
            {
                return NotFound();
            }
            return View(measure);
        }

        // POST: Measures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Value,Id,Comment")] DictMeasure measure)
        {
            if (id != measure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasureExists(measure.Id))
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
            return View(measure);
        }

        // GET: Measures/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DictMeasures == null)
            {
                return NotFound();
            }

            var measure = await _context.DictMeasures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View(measure);
        }

        // POST: Measures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DictMeasures == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.Measures'  is null.");
            }
            var measure = await _context.DictMeasures.FindAsync(id);
            if (measure != null)
            {
                _context.DictMeasures.Remove(measure);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasureExists(Guid id)
        {
          return (_context.DictMeasures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
