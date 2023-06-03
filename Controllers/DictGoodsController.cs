/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrodyagaWeb.Data;
using BrodyagaWeb.Models;

namespace BrodyagaWeb.Controllers
{
    public class DictGoodsController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public DictGoodsController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: DictGoods
        public async Task<IActionResult> Index()
        {
            return _context.DictGoods != null ?
                        View(await _context.DictGoods.
                          Include(t => t.DictGoodType).
                          Include(t => t.DictMeasure).
                          OrderBy(t => t.DictGoodType.Value).
                          ThenBy(t => t.Value).
                          ToListAsync()) :
                        Problem("Entity set 'BrodyagaWebContext.DictGood'  is null.");
        }

        private void FillViewData(DictGoods ADictGoods)
        {
            ViewData["DictGoodTypeId"] = new SelectList(_context.DictGoodType, "Id", "Value", ADictGoods.DictGoodTypeId);
            ViewData["DictMeasureId"] = new SelectList(_context.DictMeasures.OrderBy(t => t.Value), "Id", "Value", ADictGoods.DictMeasureId);
        }

        // GET: DictGoods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DictGoods == null)
            {
                return NotFound();
            }

            var vDictGoods = await _context.DictGoods
                .Include(g => g.DictMeasure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vDictGoods == null)
            {
                return NotFound();
            }

            //FillViewData(vDictGoods);
            return View(vDictGoods);
        }

        // GET: DictGoods/Create
        public IActionResult Create()
        {
            ViewData["DictGoodTypeId"] = new SelectList(_context.DictGoodType.OrderBy(t => t.Value), "Id", "Value");
            ViewData["DictMeasureId"] = new SelectList(_context.DictMeasures.OrderBy(t => t.Value), "Id", "Value");
            return View();
        }

        // POST: DictGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Comment,DictGoodTypeId,DictMeasureId")] DictGoods dictGoods)
        {
            if (ModelState.IsValid)
            {
                dictGoods.Id = Guid.NewGuid();
                _context.Add(dictGoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            FillViewData(dictGoods);
            return View(dictGoods);
        }

        // GET: DictGoods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DictGoods == null)
            {
                return NotFound();
            }

            var vDictGoods = await _context.DictGoods.FindAsync(id);
            if (vDictGoods == null)
            {
                return NotFound();
            }
            FillViewData(vDictGoods);
            return View(vDictGoods);
        }

        // POST: DictGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Value,Comment,DictGoodTypeId,DictMeasureId")] DictGoods dictGoods)
        {
            if (id != dictGoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dictGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DictGoodsExists(dictGoods.Id))
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
            FillViewData(dictGoods);
            return View(dictGoods);
        }

        // GET: DictGoods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DictGoods == null)
            {
                return NotFound();
            }

            var vDictGoods = await _context.DictGoods.
                Include(t => t.DictGoodType)
                .Include(t => t.DictMeasure)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (vDictGoods == null)
            {
                return NotFound();
            }

            return View(vDictGoods);
        }

        // POST: DictGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DictGoods == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.DictGoods'  is null.");
            }
            var vDictGoods = await _context.DictGoods.FindAsync(id);
            if (vDictGoods != null)
            {
                _context.DictGoods.Remove(vDictGoods);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DictGoodsExists(Guid id)
        {
            return (_context.DictGoods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
