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
    public class DictFighterStatesController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public DictFighterStatesController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: DictFigterStates
        public async Task<IActionResult> Index()
        {
              return _context.DictFighterStates != null ? 
                          View(await _context.DictFighterStates.OrderBy(t => t.Value).ToListAsync()) :
                          Problem("Entity set 'BrodyagaWebContext.DictFigterState'  is null.");
        }

        // GET: DictFigterStates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DictFighterStates == null)
            {
                return NotFound();
            }

            var dictFigterState = await _context.DictFighterStates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictFigterState == null)
            {
                return NotFound();
            }

            return View(dictFigterState);
        }

        // GET: DictFigterStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DictFigterStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,Id,Comment")] DictFighterState dictFigterState)
        {
            if (ModelState.IsValid)
            {
                dictFigterState.Id = Guid.NewGuid();
                _context.Add(dictFigterState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dictFigterState);
        }

        // GET: DictFigterStates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DictFighterStates == null)
            {
                return NotFound();
            }

            var dictFigterState = await _context.DictFighterStates.FindAsync(id);
            if (dictFigterState == null)
            {
                return NotFound();
            }
            return View(dictFigterState);
        }

        // POST: DictFigterStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Value,Id,Comment")] DictFighterState dictFigterState)
        {
            if (id != dictFigterState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictFigterState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictFigterStateExists(dictFigterState.Id))
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
            return View(dictFigterState);
        }

        // GET: DictFigterStates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DictFighterStates == null)
            {
                return NotFound();
            }

            var dictFigterState = await _context.DictFighterStates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dictFigterState == null)
            {
                return NotFound();
            }

            return View(dictFigterState);
        }

        // POST: DictFigterStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DictFighterStates == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.DictFigterState'  is null.");
            }
            var dictFigterState = await _context.DictFighterStates.FindAsync(id);
            if (dictFigterState != null)
            {
                _context.DictFighterStates.Remove(dictFigterState);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictFigterStateExists(Guid id)
        {
          return (_context.DictFighterStates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
