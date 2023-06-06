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
    public class SoldiersController : Controller
    {
        private readonly BrodyagaWebContext _context;

        public SoldiersController(BrodyagaWebContext context)
        {
            _context = context;
        }

        // GET: Fighters
        public async Task<IActionResult> Index()
        {
            /*
SELECT --[Id]
      [FirstName] + ' ' +
      SUBSTRING(MidleName, 1, 1) + '.' +
    SUBSTRING(LastName, 1, 1) + '.'
  FROM [Brodyaga].[dbo].[Fighters] Fig
JOIN Platoon Plt on Plt.Id = Fig.IdPlatoon
  order by FirstName
            */
            var vUnits = await _context.Units.
                Include(t => t.Users).
                OrderBy(t => t.OrderVal).
                ThenBy(t => t.ParentId).
                ThenBy(t => t.Number).ToListAsync();
            return View("IndexByUnit", vUnits);

            var vSoldiers = await _context.Users.
                Include(f => f.Unit).
                OrderBy(t => t.Unit.OrderVal).
                ThenBy(t => t.FirstName).ToListAsync();
            return View(vSoldiers);
        }

        // GET: Fighters/Details/5
        public async Task<IActionResult> Details(Guid? id, string? ActionSource)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var vUser = await _context.Users
                .Include(f => f.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vUser == null)
            {
                return NotFound();
            }

            if (ActionSource != null)
                ViewData["ActionSource"] = ActionSource;
            return View(vUser);
        }

        // GET: Fighters/Create
        public async Task<IActionResult> Create(Guid? UnitId, string? ActionSource)
        {
            if (UnitId == null)
                ViewData["Units"] = new SelectList(_context.Units.OrderBy(t => t.OrderVal), "Id", "Number");
            else
            {
                var vUnit = await _context.Units.FindAsync(UnitId);
                if (vUnit == null)
                {
                    return NotFound();
                }
                ViewData["Unit"] = vUnit;
            }
            if (ActionSource != null)
                ViewData["ActionSource"] = ActionSource;
            return View();
        }

        // POST: Fighters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MidleName,LastName,UnitId")] User soldier,
            string? ActionSource)
        {
            static string Normalize(string? AValue)
            {
                if (string.IsNullOrEmpty(AValue))
                    return string.Empty;
                AValue = AValue.Trim();
                return char.ToUpper(AValue[0]) + AValue[1..].ToLower();
            }

            if (ModelState.IsValid)
            {
                soldier.FirstName = Normalize(soldier.FirstName);
                soldier.MidleName = Normalize(soldier.MidleName);
                soldier.LastName = Normalize(soldier.LastName);

                soldier.Id = Guid.NewGuid();
                _context.Add(soldier);
                await _context.SaveChangesAsync();
                return ActionSource == null ? RedirectToAction(nameof(Index)) : Redirect(ActionSource);
            }
            ViewData["Units"] = new SelectList(_context.Units.OrderBy(t => t.OrderVal), "Id", "Number");
            return View(soldier);
        }

        // GET: Fighters/Edit/5
        public async Task<IActionResult> Edit(Guid? id, string? ActionSource)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var vSoldier = await _context.Users
                .Include(f => f.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vSoldier == null)
            {
                return NotFound();
            }
            ViewData["Units"] = new SelectList(_context.Units.OrderBy(t => t.OrderVal), "Id", "Number");
            if (ActionSource != null)
                ViewData["ActionSource"] = ActionSource;
            return View(vSoldier);
        }

        // POST: Fighters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,MidleName,LastName,UnitId")] User user,
            string? ActionSource)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldierExists(user.Id))
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
            ViewData["Units"] = new SelectList(_context.Units.OrderBy(t => t.OrderVal), "Id", "Number", user.UnitId);
            return View(user);
        }

        // GET: Fighters/Delete/5
        public async Task<IActionResult> Delete(Guid? id, string? ActionSource)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var vSoldier = await _context.Users
                .Include(f => f.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vSoldier == null)
            {
                return NotFound();
            }

            if (ActionSource != null)
                ViewData["ActionSource"] = ActionSource;
            return View(vSoldier);
        }

        // POST: Fighters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, string? ActionSource)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'BrodyagaWebContext.Fighters'  is null.");
            }
            var vSoldier = await _context.Users.FindAsync(id);
            if (vSoldier != null)
            {
                _context.Users.Remove(vSoldier);
            }
            
            await _context.SaveChangesAsync();
            return ActionSource == null ? RedirectToAction(nameof(Index)) : Redirect(ActionSource);
        }

        private bool SoldierExists(Guid id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
