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
    public class DictExtSrcsController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public DictExtSrcsController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: DictExtSrcs
        public async Task<IActionResult> Index()
        {
              return _context.DictExtSrc != null ? 
                          View(await _context.DictExtSrc.OrderBy(t => t.Value).ToListAsync()) :
                          Problem("Entity set 'BrodyagaWebContext.DictExtSrc'  is null.");
        }

        // GET: DictExtSrcs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DictExtSrc == null)
            {
                return NotFound();
            }

            var dictExtSrc = await _context.DictExtSrc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictExtSrc == null)
            {
                return NotFound();
            }

            return View(dictExtSrc);
        }

        // GET: DictExtSrcs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DictExtSrcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Comment")] DictExtSrc dictExtSrc)
        {
            if (ModelState.IsValid)
            {
                dictExtSrc.Id = Guid.NewGuid();
                _context.Add(dictExtSrc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dictExtSrc);
        }

        // GET: DictExtSrcs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DictExtSrc == null)
            {
                return NotFound();
            }

            var dictExtSrc = await _context.DictExtSrc.FindAsync(id);
            if (dictExtSrc == null)
            {
                return NotFound();
            }
            return View(dictExtSrc);
        }

        // POST: DictExtSrcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Value,Comment")] DictExtSrc dictExtSrc)
        {
            if (id != dictExtSrc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictExtSrc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictExtSrcExists(dictExtSrc.Id))
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
            return View(dictExtSrc);
        }

        // GET: DictExtSrcs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DictExtSrc == null)
            {
                return NotFound();
            }

            var dictExtSrc = await _context.DictExtSrc
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictExtSrc == null)
            {
                return NotFound();
            }

            return View(dictExtSrc);
        }

        // POST: DictExtSrcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DictExtSrc == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.DictExtSrc'  is null.");
            }
            var dictExtSrc = await _context.DictExtSrc.FindAsync(id);
            if (dictExtSrc != null)
            {
                _context.DictExtSrc.Remove(dictExtSrc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictExtSrcExists(Guid id)
        {
          return (_context.DictExtSrc?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
