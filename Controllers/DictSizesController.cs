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

namespace BrodyagaWeb.Controllers
{
    public class DictSizesController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public DictSizesController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: DictSizes
        public async Task<IActionResult> Index()
        {
              return _context.DictSize != null ? 
                          View(await _context.DictSize.OrderBy(t => t.Value).ToListAsync()) :
                          Problem("Entity set 'BrodyagaWebContext.DictSize'  is null.");
        }

        // GET: DictSizes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DictSize == null)
            {
                return NotFound();
            }

            var dictSize = await _context.DictSize
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictSize == null)
            {
                return NotFound();
            }

            return View(dictSize);
        }

        // GET: DictSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DictSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Comment")] DictSize dictSize)
        {
            if (ModelState.IsValid)
            {
                dictSize.Id = Guid.NewGuid();
                _context.Add(dictSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dictSize);
        }

        // GET: DictSizes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DictSize == null)
            {
                return NotFound();
            }

            var dictSize = await _context.DictSize.FindAsync(id);
            if (dictSize == null)
            {
                return NotFound();
            }
            return View(dictSize);
        }

        // POST: DictSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Value,Comment")] DictSize dictSize)
        {
            if (id != dictSize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictSizeExists(dictSize.Id))
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
            return View(dictSize);
        }

        // GET: DictSizes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DictSize == null)
            {
                return NotFound();
            }

            var dictSize = await _context.DictSize
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictSize == null)
            {
                return NotFound();
            }

            return View(dictSize);
        }

        // POST: DictSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DictSize == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.DictSize'  is null.");
            }
            var dictSize = await _context.DictSize.FindAsync(id);
            if (dictSize != null)
            {
                _context.DictSize.Remove(dictSize);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictSizeExists(Guid id)
        {
          return (_context.DictSize?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
