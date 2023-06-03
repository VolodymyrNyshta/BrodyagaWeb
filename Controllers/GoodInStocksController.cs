/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IO;
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrodyagaWeb.Data;
using BrodyagaWeb.Models;

namespace BrodyagaWeb.Controllers
{
    public class GoodInStocksController : Controller
    {
        private readonly BrodyagaWebContext _context;
        private readonly IWebHostEnvironment _appEnv;
        private string ImgsPath => _appEnv.WebRootPath + "/imgs/";

        public GoodInStocksController(BrodyagaWebContext context, IWebHostEnvironment appEnv)
        {
            _context = context;
            _appEnv = appEnv;
        }

        private async Task<string> SaveImageFile(IFormFile GoodImage)
        {
            if (GoodImage == null)
                return string.Empty;

            var vFileName = GoodImage.FileName;//vId.ToString() + new FileInfo(AGoodImage.FileName).Extension;
            var vPath = ImgsPath + vFileName;
            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(vPath, FileMode.Create))
            {
                await GoodImage.CopyToAsync(fileStream);
            }
            return vFileName;
        }

        private async Task<bool> NewImage(GoodInStock goodInStock, IFormFile GoodImage)
        {
            if (GoodImage == null)
                return false;
            /*
            if (_context.GoodInStock == null)
            {
                return false;//NotFound();
            }

            var vGoodInStock = await _context.GoodInStock.
                FirstOrDefaultAsync(m => m.Id == GoodId);
            if (vGoodInStock == null)
            {
                return false;//NotFound();
            }
            */

            var vId = Guid.NewGuid();
            var vFileName = await SaveImageFile(GoodImage);
            var vGoodImage = new GoodImage
            {
                Id = vId,
                FileName = vFileName,
                //Comment = AGoodInStock.DictGoodsValue
            };
            _context.GoodImage.Add(vGoodImage);
            var vGoodInStockImage = new GoodInStockImage()
            {
                Id = Guid.NewGuid(),
                GoodImageId = vId,
                GoodInStockId = goodInStock.Id,
                GoodInStock = goodInStock
            };
            _context.GoodInStockImage.Add(vGoodInStockImage);

            return true;//Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EdtActNewImage(Guid GoodId, IFormFile GoodImage, string? ActionSource)
        {
            if (GoodImage == null)
                return NotFound();
            var vGoodInStock = await _context.GoodInStock
                .FirstOrDefaultAsync(m => m.Id == GoodId);
            if (vGoodInStock == null)
                return NotFound();

            var vImgResult = await NewImage(vGoodInStock, GoodImage);
            if(vImgResult)
                await _context.SaveChangesAsync();

            return vImgResult ? RedirectToAction(nameof(Edit), new { id = GoodId, ActionSource }) : NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EdtActChangeImage(Guid GoodId, Guid ImageId,
            IFormFile GoodImage, string? ActionSource)
        {
            if (GoodImage == null)
                return NotFound();

            var vGoodImage = await _context.GoodImage.
                FirstOrDefaultAsync(t => t.Id == ImageId);
            if (vGoodImage == null)
            {
                return NotFound();
            }
            var vFileName = await SaveImageFile(GoodImage);
            vGoodImage.FileName = vFileName;
            _context.GoodImage.Update(vGoodImage);

            var vGoodInStockImage = await _context.GoodInStockImage.
                FirstOrDefaultAsync(t => t.GoodInStockId == GoodId && t.GoodImageId == ImageId);
            if (vGoodInStockImage == null)
            {
                return NotFound();
            }
            vGoodInStockImage.GoodImageId = vGoodImage.Id;
            _context.GoodInStockImage.Update(vGoodInStockImage);

            /*
            if (_context.GoodInStock == null)
            {
                return NotFound();
            }
            var vGoodInStock = await _context.GoodInStock.
                FirstOrDefaultAsync(m => m.Id == GoodId);
            if (vGoodInStock == null)
            {
                return NotFound();
            }
             */
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = GoodId, ActionSource });
        }

        public async Task<IActionResult> EdtActDeleteImage(Guid GoodId, Guid ImageId, string? ActionSource)
        {
            var vGoodImage = await _context.GoodImage.
                FirstOrDefaultAsync(t => t.Id == ImageId);
            if (vGoodImage == null)
            {
                return NotFound();
            }

            var vGoodInStockImage = await _context.GoodInStockImage.
                FirstOrDefaultAsync(t => t.GoodInStockId == GoodId && t.GoodImageId == ImageId);
            if (vGoodInStockImage == null)
            {
                return NotFound();
            }

            var vFileInf = new FileInfo(ImgsPath + vGoodImage.FileName);
            if (vFileInf.Exists)
                vFileInf.Delete();

            _context.GoodInStockImage.Remove(vGoodInStockImage);
            _context.GoodImage.Remove(vGoodImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = GoodId, ActionSource });
        }

        // GET: GoodInStocks
        public async Task<IActionResult> Index()
        {
            var vGoodInStock = _context.GoodInStock.
                Include(g => g.DictGoods).
                //ThenInclude(t => t.DictMeasure).
                Include(g => g.DictGoods.DictMeasure).
                Include(g => g.DictSize).
                Include(g => g.TransferAct);
            return View(await vGoodInStock.ToListAsync());
        }

        private async Task FillViewDataImgs(GoodInStock AGoodInStock)
        {
            var vImages = await (from Img in _context.GoodImage
                                 join GdInStck in _context.GoodInStockImage on Img.Id equals GdInStck.GoodImageId
                                 where GdInStck.GoodInStockId == AGoodInStock.Id
                                 select Img).ToListAsync();
            ViewData["Images"] = vImages;
        }

        private async Task FillViewData(GoodInStock AGoodInStock)
        {
            ViewData["DictGoodsId"] = new SelectList(_context.DictGoods.OrderBy(t => t.Value), "Id", "Value", AGoodInStock.DictGoodsId);
            ViewData["DictSizeId"] = new SelectList(_context.DictSize.OrderBy(t => t.Value), "Id", "Value", AGoodInStock.DictSizeId);
            //ViewData["DictMeasureId"] = new SelectList(_context.DictMeasures.OrderBy(t => t.Value), "Id", "Value", AGoodInStock.DictMeasureId);
            ViewData["TransferActId"] = new SelectList(_context.TransferAct.OrderBy(t => t.ActNumber), "Id", "ActNumber", AGoodInStock.TransferActId);
            await FillViewDataImgs(AGoodInStock);
        }

        // GET: GoodInStocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.GoodInStock == null)
            {
                return NotFound();
            }

            var goodInStock = await _context.GoodInStock
                .Include(g => g.DictGoods)
                .Include(g => g.DictSize)
                .Include(g => g.DictGoods.DictMeasure)
                .Include(g => g.TransferAct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodInStock == null)
            {
                return NotFound();
            }

            await FillViewDataImgs(goodInStock);
            return View(goodInStock);
        }

        // GET: GoodInStocks/Create
        public async Task<IActionResult> Create(Guid? transferActId, string? ActionSource)
        {
            ViewData["DictGoodsId"] = new SelectList(_context.DictGoods.OrderBy(t => t.Value), "Id", "Value");
            ViewData["DictSizeId"] = new SelectList(_context.DictSize.OrderBy(t => t.Value), "Id", "Value");
            if (transferActId == null)
                ViewData["TransferActList"] = new SelectList(_context.TransferAct.OrderBy(t => t.ActNumber), "Id", "ActNumber");
            else
            {
                var vTransferAct = await _context.TransferAct.FindAsync(transferActId);
                if (vTransferAct == null)
                {
                    return NotFound();
                }
                ViewData["TransferAct"] = vTransferAct;
            }
            ViewData["Images"] = new List<GoodImage>();
            if (ActionSource != null)
                ViewData["ActionSource"] = ActionSource;

            return View();
        }

        // POST: GoodInStocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            IFormFile? GoodImage,
            [Bind("Id,Comment,Price,Cnt,DictSizeId,DictGoodsId,TransferActId")] GoodInStock goodInStock,
            string? ActionSource)
        {
            if (ModelState.IsValid)
            {
                goodInStock.Id = Guid.NewGuid();
                _context.GoodInStock.Add(goodInStock);
                if (GoodImage != null)
                    await NewImage(goodInStock, GoodImage);
                await _context.SaveChangesAsync();

                return ActionSource == null ? RedirectToAction(nameof(Index)) : Redirect(ActionSource);
            }
            await FillViewData(goodInStock);
            return View(goodInStock);
        }

        // GET: GoodInStocks/Edit/5
        public async Task<IActionResult> Edit(Guid? id, string? ActionSource)
        {
            if (id == null || _context.GoodInStock == null)
            {
                return NotFound();
            }

            var goodInStock = await _context.GoodInStock.
                Include(goodInStock => goodInStock.DictGoods).
                Include(goodInStock => goodInStock.DictGoods.DictMeasure).
                FirstOrDefaultAsync(m => m.Id == id);
            if (goodInStock == null)//
            {
                return NotFound();
            }
            await FillViewData(goodInStock);
            if (ActionSource != null)
                ViewData["ActionSource"] = ActionSource;
            return View(goodInStock);
        }

        // POST: GoodInStocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Comment,Price,Cnt,DictSizeId,DictGoodsId,DictMeasureId,TransferActId")] GoodInStock goodInStock,
            string? ActionSource)
        {
            if (id != goodInStock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodInStock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodInStockExists(goodInStock.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return ActionSource == null ? RedirectToAction(nameof(Index)) : Redirect(ActionSource);
            }
            await FillViewData(goodInStock);
            return View(goodInStock);
        }

        // GET: GoodInStocks/Delete/5
        public async Task<IActionResult> Delete(Guid? id, string? ActionSource)
        {
            if (id == null || _context.GoodInStock == null)
            {
                return NotFound();
            }

            var goodInStock = await _context.GoodInStock
                .Include(g => g.DictGoods)
                .Include(g => g.DictSize)
                .Include(g => g.DictGoods.DictMeasure)
                .Include(g => g.TransferAct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodInStock == null)
            {
                return NotFound();
            }

            if (ActionSource != null)
                ViewData["ActionSource"] = ActionSource;
            return View(goodInStock);
        }

        // POST: GoodInStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, string? ActionSource)
        {
            if (_context.GoodInStock == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.GoodInStock'  is null.");
            }
            var goodInStock = await _context.GoodInStock.FindAsync(id);
            if (goodInStock != null)
            {
                _context.GoodInStock.Remove(goodInStock);
            }

            await _context.SaveChangesAsync();
            return ActionSource == null ? RedirectToAction(nameof(Index)) : Redirect(ActionSource);
        }

        private bool GoodInStockExists(Guid id)
        {
            return (_context.GoodInStock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
