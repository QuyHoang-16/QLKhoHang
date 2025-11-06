using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
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
            if (!_context.Branches.Any())
            {
                ViewData["branchId"] = new SelectList(Enumerable.Empty<Branch>(), "branchId", "branchName");
                ViewData["StatusMessage"] = "Chưa có Chi nhánh. Hãy tạo Chi nhánh trước khi tạo Kho.";
                return View();
            }

            var defaultBranch = _context.Branches.FirstOrDefault(x => x.isDefaultBranch == true);
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName", defaultBranch != null ? defaultBranch.branchId : null);
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("branchId,warehouseId,warehouseName,description,street1,street2,city,province,country,createdAt")] Warehouse warehouse)
        {
            var branchExists = _context.Branches.Any(b => b.branchId == warehouse.branchId);
            if (!branchExists)
            {
                ModelState.AddModelError("branchId", "Chi nhánh không tồn tại hoặc đã bị xóa.");
            }

            if (branchExists && ModelState.ContainsKey(nameof(warehouse.branchId)))
            {
                ModelState[nameof(warehouse.branchId)].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(warehouse);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Không thể tạo kho. Vui lòng kiểm tra dữ liệu và thử lại.");
                }
            }
            
            var allErrors = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            if (!string.IsNullOrWhiteSpace(allErrors))
            {
                ViewData["StatusMessage"] = allErrors;
            }
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName", warehouse.branchId);
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


namespace QuanLyKho.MVC
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
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Warehouse")]
        public bool WarehouseRole { get; set; } = false;
    }
}
