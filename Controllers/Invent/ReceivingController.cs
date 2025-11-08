using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
{
    [Authorize(Roles = "Receiving")]
    public class ReceivingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceivingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetWarehouseByOrder(string purchaseOrderId)
        {
            if (string.IsNullOrWhiteSpace(purchaseOrderId) || purchaseOrderId == "0")
            {
                var empty = new List<Warehouse> { new Warehouse { warehouseId = "0", warehouseName = "Select" } };
                return Json(new SelectList(empty, "warehouseId", "warehouseName"));
            }

            var po = _context.PurchaseOrders
                .Include(x => x.branch)
                .FirstOrDefault(x => x.purchaseOrderId == purchaseOrderId);

            if (po == null || po.branch == null)
            {
                var empty = new List<Warehouse> { new Warehouse { warehouseId = "0", warehouseName = "Select" } };
                return Json(new SelectList(empty, "warehouseId", "warehouseName"));
            }

            var warehouseList = _context.Warehouses
                .Where(x => x.branchId == po.branch.branchId)
                .ToList();
            warehouseList.Insert(0, new Warehouse { warehouseId = "0", warehouseName = "Select" });

            return Json(new SelectList(warehouseList, "warehouseId", "warehouseName"));
        }

        public async Task<IActionResult> ShowGSRN(string id)
        {
            Receiving obj = await _context.Receivings
                .Include(x => x.vendor)
                .Include(x => x.purchaseOrder)
                    .ThenInclude(x => x.branch)
                .Include(x => x.receivingLine).ThenInclude(x => x.product)
                .SingleOrDefaultAsync(x => x.receivingId.Equals(id));
            return View(obj);
        }

        public async Task<IActionResult> PrintGSRN(string id)
        {
            Receiving obj = await _context.Receivings
                .Include(x => x.vendor)
                .Include(x => x.purchaseOrder)
                    .ThenInclude(x => x.branch)
                .Include(x => x.receivingLine).ThenInclude(x => x.product)
                .SingleOrDefaultAsync(x => x.receivingId.Equals(id));
            return View(obj);
        }

        // GET: Receiving
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Receivings.OrderByDescending(x => x.createdAt).Include(r => r.branch).Include(r => r.purchaseOrder).Include(r => r.vendor).Include(r => r.warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Receiving/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiving = await _context.Receivings
                    .Include(r => r.branch)
                    .Include(r => r.purchaseOrder)
                    .Include(r => r.vendor)
                    .Include(r => r.warehouse)
                        .SingleOrDefaultAsync(m => m.receivingId == id);
            if (receiving == null)
            {
                return NotFound();
            }

            return View(receiving);
        }


        // GET: Receiving/Create
        public IActionResult Create()
        {
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName");
            // Prefer Open POs; if none, fall back to non-Completed to aid data entry
            var poList = _context.PurchaseOrders
                .Where(x => x.purchaseOrderStatus == PurchaseOrderStatus.Open)
                .ToList();
            if (!poList.Any())
            {
                poList = _context.PurchaseOrders
                    .Where(x => x.purchaseOrderStatus != PurchaseOrderStatus.Completed)
                    .ToList();
            }
            poList.Insert(0, new PurchaseOrder { purchaseOrderId = "0", purchaseOrderNumber = "Select" });
            ViewData["purchaseOrderId"] = new SelectList(poList, "purchaseOrderId", "purchaseOrderNumber");
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorName");
            ViewData["warehouseId"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
            Receiving rcv = new Receiving();
            return View(rcv);
        }




        // POST: Receiving/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("purchaseOrderId,receivingNumber,receivingDate,vendorId,vendorDO,vendorInvoice,branchId,warehouseId")] Receiving receiving)
        {
            if (receiving.purchaseOrderId == "0" || receiving.warehouseId == "0")
            {
                TempData["StatusMessage"] = "Error. Purchase order or warehouse is not valid. Please select valid purchase order and warehouse";
                return RedirectToAction(nameof(Create));
            }

            if (ModelState.IsValid)
            {
                //check receiving
                Receiving check = await _context.Receivings.SingleOrDefaultAsync(x => x.purchaseOrderId.Equals(receiving.purchaseOrderId));
                if (check != null)
                {
                    ViewData["StatusMessage"] = "Error. Purchase order already received. " + check.receivingNumber;

                    ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName");
                    ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrders, "purchaseOrderId", "purchaseOrderNumber");
                    ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorName");
                    ViewData["warehouseId"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");

                    return View(receiving);
                }
                receiving.warehouse = await _context.Warehouses.Include(x => x.branch).SingleOrDefaultAsync(x => x.warehouseId.Equals(receiving.warehouseId));
                receiving.branch = receiving.warehouse?.branch;
                receiving.branchId = receiving.warehouse?.branchId;
                receiving.purchaseOrder = await _context.PurchaseOrders.Include(x => x.vendor).SingleOrDefaultAsync(x => x.purchaseOrderId.Equals(receiving.purchaseOrderId));
                receiving.vendor = receiving.purchaseOrder?.vendor;

                _context.Add(receiving);

                //change status of purchase order to completed
                receiving.purchaseOrder.purchaseOrderStatus = PurchaseOrderStatus.Completed;
                _context.PurchaseOrders.Update(receiving.purchaseOrder);

                await _context.SaveChangesAsync();

                //auto create receiving line, full receive
                List<PurchaseOrderLine> polines = new List<PurchaseOrderLine>();
                polines = _context.PurchaseOrderLines.Include(x => x.product).Where(x => x.purchaseOrderId.Equals(receiving.purchaseOrderId)).ToList();
                foreach (var item in polines)
                {
                    ReceivingLine line = new ReceivingLine();
                    line.receiving = receiving;
                    line.product = item.product;
                    line.qty = item.qty;
                    line.qtyReceive = item.qty;
                    line.qtyInventory = line.qtyReceive * 1;
                    line.branchId = receiving.branchId;
                    line.warehouseId = receiving.warehouseId;

                    _context.ReceivingLines.Add(line);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Details), new { id = receiving.receivingId });
            }
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName", receiving.branchId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrders, "purchaseOrderId", "purchaseOrderNumber", receiving.purchaseOrderId);
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorName", receiving.vendorId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName", receiving.warehouseId);
            return View(receiving);
        }

        // GET: Receiving/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiving = await _context.Receivings.SingleOrDefaultAsync(m => m.receivingId == id);
            if (receiving == null)
            {
                return NotFound();
            }
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName", receiving.branchId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrders, "purchaseOrderId", "purchaseOrderNumber", receiving.purchaseOrderId);
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorName", receiving.vendorId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName", receiving.warehouseId);
            return View(receiving);
        }

        // POST: Receiving/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("receivingId,purchaseOrderId,receivingNumber,receivingDate,vendorId,vendorDO,vendorInvoice,branchId,warehouseId,HasChild,createdAt")] Receiving receiving)
        {
            if (id != receiving.receivingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceivingExists(receiving.receivingId))
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
            ViewData["branchId"] = new SelectList(_context.Branches, "branchId", "branchName", receiving.branchId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrders, "purchaseOrderId", "purchaseOrderNumber", receiving.purchaseOrderId);
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorName", receiving.vendorId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName", receiving.warehouseId);
            return View(receiving);
        }

        // GET: Receiving/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiving = await _context.Receivings
                    .Include(r => r.branch)
                    .Include(r => r.purchaseOrder)
                    .Include(r => r.vendor)
                    .Include(r => r.warehouse)
                    .SingleOrDefaultAsync(m => m.receivingId == id);
            if (receiving == null)
            {
                return NotFound();
            }

            return View(receiving);
        }




        // POST: Receiving/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var receiving = await _context.Receivings
                .Include(x => x.receivingLine)
                .Include(x => x.purchaseOrder)
                .SingleOrDefaultAsync(m => m.receivingId == id);
            try
            {
                _context.ReceivingLines.RemoveRange(receiving.receivingLine);
                _context.Receivings.Remove(receiving);

                //rollback status to open
                receiving.purchaseOrder.purchaseOrderStatus = PurchaseOrderStatus.Open;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Error. Calm Down ^_^ and please contact your SysAdmin with this message: " + ex;
                return View(receiving);
            }

        }

        private bool ReceivingExists(string id)
        {
            return _context.Receivings.Any(e => e.receivingId == id);
        }

    }
}


namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class Receiving
        {
            public const string Controller = "Receiving";
            public const string Action = "Index";
            public const string Role = "Receiving";
            public const string Url = "/Receiving/Index";
            public const string Name = "Receiving";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Receiving")]
        public bool ReceivingRole { get; set; } = false;
    }
}
