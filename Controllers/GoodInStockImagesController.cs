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
    public class GoodInStockImagesController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public GoodInStockImagesController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: GoodInStockImages
        public async Task<IActionResult> Index()
        {
            if(_context.GoodInStockImage == null)
                return Problem("Entity set 'BrodyagaWebContext.GoodInStockImage'  is null.");

            var vLst = await _context.GoodInStockImage.
                Include(t => t.GoodInStock).
                Include(t => t.GoodInStock.DictGoods).
                Include(t => t.GoodImage).
                ToListAsync();
            return View(vLst);
                        
            /*
            return _context.GoodInStockImage != null ? 
                          View(await _context.GoodInStockImage.ToListAsync()) :
                          Problem("Entity set 'BrodyagaWebContext.GoodInStockImage'  is null.");
            */
        }

        // GET: GoodInStockImages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.GoodInStockImage == null)
            {
                return NotFound();
            }

            var goodInStockImage = await _context.GoodInStockImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodInStockImage == null)
            {
                return NotFound();
            }

            return View(goodInStockImage);
        }

        // GET: GoodInStockImages/Create
        public async Task<IActionResult> Create()
        {
            var vGoodInStock = await _context.GoodInStock.
                Include(t => t.DictGoods).
                OrderBy(t => t.DictGoods.Value).
                ToListAsync();
            var vGoodImage = await _context.GoodImage.ToListAsync();

            ViewData["GoodInStockId"] = new SelectList(vGoodInStock, "Id", "DictGoodsValue");
            ViewData["GoodImageId"] = new SelectList(vGoodImage, "Id", "ImageUrl");
            return View();
        }

        // POST: GoodInStockImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GoodImageId,GoodInStockId")] GoodInStockImage goodInStockImage)
        {
            if (ModelState.IsValid)
            {
                goodInStockImage.Id = Guid.NewGuid();
                _context.Add(goodInStockImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goodInStockImage);
        }

        // GET: GoodInStockImages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.GoodInStockImage == null)
            {
                return NotFound();
            }

            var goodInStockImage = await _context.GoodInStockImage.FindAsync(id);
            if (goodInStockImage == null)
            {
                return NotFound();
            }
            return View(goodInStockImage);
        }

        // POST: GoodInStockImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,GoodImageId,GoodsInStockId")] GoodInStockImage goodInStockImage)
        {
            if (id != goodInStockImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodInStockImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodInStockImageExists(goodInStockImage.Id))
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
            return View(goodInStockImage);
        }

        // GET: GoodInStockImages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.GoodInStockImage == null)
            {
                return NotFound();
            }

            var goodInStockImage = await _context.GoodInStockImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodInStockImage == null)
            {
                return NotFound();
            }

            return View(goodInStockImage);
        }

        // POST: GoodInStockImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.GoodInStockImage == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.GoodInStockImage'  is null.");
            }
            var goodInStockImage = await _context.GoodInStockImage.FindAsync(id);
            if (goodInStockImage != null)
            {
                _context.GoodInStockImage.Remove(goodInStockImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodInStockImageExists(Guid id)
        {
          return (_context.GoodInStockImage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
