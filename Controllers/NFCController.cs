//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrodyagaWeb.Models;
using static System.Collections.Specialized.BitVector32;

namespace BrodyagaWeb.Controllers
{
    public class NFCController : Controller
    {
        [HttpGet]
        public ActionResult ProcessToken(int SectionId, Guid Id)
        {
/*
            NFCToken ANFCToken = new() { Id = Id, SectionId = (TSectionId)SectionId };
            return View(ANFCToken);
*/
            try
            {
                return (TSectionId)SectionId switch
                {
                    TSectionId.Fighter => RedirectToAction(nameof(SoldiersController.Details),
                                                "Fighters", new { id = Id }),
                    TSectionId.GoodInStock => RedirectToAction(nameof(GoodInStocksController.Details),
                                                "GoodInStocks", new { id = Id }),
                    TSectionId.TransferAct => RedirectToAction(nameof(TransferActsController.Details),
                                                "TransferActs", new { id = Id }),
                    _ => new BadRequestResult(),
                };
            }
            catch
            {
                return View();
            }
        }

        /*
        [HttpPost]
        public ActionResult ProcessToken([FromBody] NFCToken ANFCToken)
        {
            try
            {
                return ANFCToken.SectionId switch
                {
                    TSectionId.Fighter => RedirectToAction(nameof(FightersController.Details),
                                                "Fighters", new { id = ANFCToken.Id }),
                    TSectionId.GoodInStock => RedirectToAction(nameof(GoodInStocksController.Edit),
                                                "GoodInStocks", new { id = ANFCToken.Id }),
                    TSectionId.TransferAct => RedirectToAction(nameof(TransferActsController.Details),
                                                "TransferActs", new { id = ANFCToken.Id }),
                    _ => new BadRequestResult(),
                };
            }
            catch
            {
                return View();
            }
        }
        */

        // GET: NFCController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NFCController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NFCController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NFCController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NFCController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NFCController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NFCController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NFCController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
