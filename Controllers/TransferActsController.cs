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
    public class TransferActsController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public TransferActsController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: TransferActs
        public async Task<IActionResult> Index()
        {
            var brodyagaWebContext = _context.TransferAct.
                Include(t => t.DictExtSrc).
                OrderBy(t => t.ActNumber);
            return View(await brodyagaWebContext.ToListAsync());
        }

        // GET: TransferActs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TransferAct == null)
            {
                return NotFound();
            }

            var transferAct = await _context.TransferAct
                .Include(t => t.DictExtSrc)
                //.Include(t => t.GoodsInStock)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transferAct == null)
            {
                return NotFound();
            }

            var vGoodsInStock = await _context.GoodInStock.
                Where(t => t.TransferActId == id).
                Include(t => t.DictGoods).
                Include(t => t.DictSize).
                Include(t => t.DictGoods.DictMeasure).
                ToListAsync();
            ViewData["GoodsInStock"] = vGoodsInStock;
            return View(transferAct);
        }

        // GET: TransferActs/Create
        public IActionResult Create()
        {
            ViewData["DictExtSrcId"] = new SelectList(_context.DictExtSrc, "Id", "Value");
            return View();
        }

        // POST: TransferActs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActNumber,Comment,DictExtSrcId,IsGiveOut,OrderDate")] TransferAct transferAct)
        {
            if (ModelState.IsValid)
            {
                transferAct.Id = Guid.NewGuid();
                _context.Add(transferAct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DictExtSrcId"] = new SelectList(_context.DictExtSrc.OrderBy(t => t.Value), "Id", "Value", transferAct.DictExtSrcId);
            return View(transferAct);
        }

        // GET: TransferActs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TransferAct == null)
            {
                return NotFound();
            }

            var transferAct = await _context.TransferAct.FindAsync(id);
            if (transferAct == null)
            {
                return NotFound();
            }
            ViewData["DictExtSrcId"] = new SelectList(_context.DictExtSrc.OrderBy(t => t.Value), "Id", "Value", transferAct.DictExtSrcId);
            return View(transferAct);
        }

        // POST: TransferActs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ActNumber,Comment,DictExtSrcId,IsGiveOut,OrderDate,RegisterDate")] TransferAct transferAct)
        {
            if (id != transferAct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferAct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferActExists(transferAct.Id))
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
            ViewData["DictExtSrcId"] = new SelectList(_context.DictExtSrc.OrderBy(t => t.Value), "Id", "Value", transferAct.DictExtSrcId);
            return View(transferAct);
        }

        // GET: TransferActs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TransferAct == null)
            {
                return NotFound();
            }

            var transferAct = await _context.TransferAct
                .Include(t => t.DictExtSrc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transferAct == null)
            {
                return NotFound();
            }

            return View(transferAct);
        }

        // POST: TransferActs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TransferAct == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.TransferAct'  is null.");
            }
            var transferAct = await _context.TransferAct.FindAsync(id);
            if (transferAct != null)
            {
                var vGoodsInStock = await _context.GoodInStock.
                    Where(t => t.TransferActId == id).
                    Include(t => t.Images).
                    ToListAsync();
                if (vGoodsInStock != null)
                {
                    _context.GoodInStock.RemoveRange(vGoodsInStock);
                }
                _context.TransferAct.Remove(transferAct);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TransferActExists(Guid id)
        {
          return (_context.TransferAct?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
