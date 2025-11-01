using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCore_Update.Data;
using NetCore_Update.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace NetCore_Update.Controllers.Invent
{
    [Authorize(Roles = "Warehouse")]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
            return View(await _context.Warehouses.OrderByDescending(x => x.createdAt).Include(x => x.branch).ToListAsync());
        }

        // GET: Warehouse/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                        .Include(x => x.branch)
                        .SingleOrDefaultAsync(m => m.warehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }


        // GET: Warehouse/Create
        public IActionResult Create()
        {
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName");
            return View();
        }




        // POST: Warehouse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("branchId,warehouseId,warehouseName,description,street1,street2,city,province,country,createdAt")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouse/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses.Include(x => x.branch).SingleOrDefaultAsync(m => m.warehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName", warehouse.branchId);
            return View(warehouse);
        }

        // POST: Warehouse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("branchId,warehouseId,warehouseName,description,street1,street2,city,province,country,createdAt")] Warehouse warehouse)
        {
            if (id != warehouse.warehouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.warehouseId))
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
            return View(warehouse);
        }

        // GET: Warehouse/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                    .Include(x => x.branch)
                    .SingleOrDefaultAsync(m => m.warehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }




        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var warehouse = await _context.Warehouses.SingleOrDefaultAsync(m => m.warehouseId == id);
            try
            {
                _context.Warehouses.Remove(warehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Error. Calm Down ^_^ and please contact your SysAdmin with this message: " + ex;
                return View(warehouse);
            }

        }

        private bool WarehouseExists(string id)
        {
            return _context.Warehouses.Any(e => e.warehouseId == id);
        }

    }
}


namespace NetCore_Update.MVC
{
    public static partial class Pages
    {
        public static class Warehouse
        {
            public const string Controller = "Warehouse";
            public const string Action = "Index";
            public const string Role = "Warehouse";
            public const string Url = "/Warehouse/Index";
            public const string Name = "Warehouse";
        }
    }
}
namespace NetCore_Update.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Warehouse")]
        public bool WarehouseRole { get; set; } = false;
    }
}
