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
    public class DictGoodTypesController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public DictGoodTypesController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: DictGoodTypes
        public async Task<IActionResult> Index()
        {
              return _context.DictGoodType != null ? 
                          View(await _context.DictGoodType.OrderBy(t => t.Value).ToListAsync()) :
                          Problem("Entity set 'BrodyagaWebContext.DictGoodType'  is null.");
        }

        // GET: DictGoodTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DictGoodType == null)
            {
                return NotFound();
            }

            var dictGoodType = await _context.DictGoodType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictGoodType == null)
            {
                return NotFound();
            }

            return View(dictGoodType);
        }

        // GET: DictGoodTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DictGoodTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Comment")] DictGoodType dictGoodType)
        {
            if (ModelState.IsValid)
            {
                dictGoodType.Id = Guid.NewGuid();
                _context.Add(dictGoodType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dictGoodType);
        }

        // GET: DictGoodTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DictGoodType == null)
            {
                return NotFound();
            }

            var dictGoodType = await _context.DictGoodType.FindAsync(id);
            if (dictGoodType == null)
            {
                return NotFound();
            }
            return View(dictGoodType);
        }

        // POST: DictGoodTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Value,Comment")] DictGoodType dictGoodType)
        {
            if (id != dictGoodType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictGoodType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictGoodTypeExists(dictGoodType.Id))
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
            return View(dictGoodType);
        }

        // GET: DictGoodTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DictGoodType == null)
            {
                return NotFound();
            }

            var dictGoodType = await _context.DictGoodType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictGoodType == null)
            {
                return NotFound();
            }

            return View(dictGoodType);
        }

        // POST: DictGoodTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DictGoodType == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.DictGoodType'  is null.");
            }
            var dictGoodType = await _context.DictGoodType.FindAsync(id);
            if (dictGoodType != null)
            {
                _context.DictGoodType.Remove(dictGoodType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictGoodTypeExists(Guid id)
        {
          return (_context.DictGoodType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
