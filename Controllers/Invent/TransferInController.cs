using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
{
    [Authorize(Roles = "TransferIn")]
    public class TransferInController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransferInController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowTransferIn(string id)
        {
            TransferIn obj = await _context.TransferIns
                .Include(x => x.transferOrder)
                .Include(x => x.warehouseFrom)
                .Include(x => x.warehouseTo)
                .Include(x => x.transferInLine).ThenInclude(x => x.product)
                .SingleOrDefaultAsync(x => x.transferInId.Equals(id));


            return View(obj);
        }

        public async Task<IActionResult> PrintTransferIn(string id)
        {
            TransferIn obj = await _context.TransferIns
                .Include(x => x.transferOrder)
                .Include(x => x.warehouseFrom)
                .Include(x => x.warehouseTo)
                .Include(x => x.transferInLine).ThenInclude(x => x.product)
                .SingleOrDefaultAsync(x => x.transferInId.Equals(id));

            return View(obj);
        }

        // GET: TransferIn
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TransferIns.OrderByDescending(x => x.createdAt).Include(t => t.transferOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TransferIn/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferIn = await _context.TransferIns
                    .Include(x => x.branchFrom)
                    .Include(x => x.branchTo)
                    .Include(x => x.warehouseFrom)
                    .Include(x => x.warehouseTo)
                    .Include(t => t.transferOrder)
                        .SingleOrDefaultAsync(m => m.transferInId == id);
            if (transferIn == null)
            {
                return NotFound();
            }

            return View(transferIn);
        }


        // GET: TransferIn/Create
        public IActionResult Create()
        {
            var toList = _context.TransferOrders.Where(x => x.transferOrderStatus == TransferOrderStatus.Open && x.isIssued == true && x.isReceived == false).ToList();
            if (!toList.Any())
            {
                toList = _context.TransferOrders.Where(x => x.isIssued == true && x.isReceived == false).ToList();
            }
            toList.Insert(0, new TransferOrder { transferOrderId = "0", transferOrderNumber = "Select" });
            ViewData["transferOrderId"] = new SelectList(toList, "transferOrderId", "transferOrderNumber");
            ViewData["branchIdFrom"] = new SelectList(_context.Branches, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branches, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
            TransferIn obj = new TransferIn();
            return View(obj);
        }




        // POST: TransferIn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transferOrderId,transferInNumber,transferInDate,description")] TransferIn transferIn)
        {
            if (transferIn.transferOrderId == "0")
            {
                TempData["StatusMessage"] = "Error. Vui lòng chọn Phiếu điều chuyển hợp lệ.";
                return RedirectToAction(nameof(Create));
            }
            // derive related fields before validating
            var to = await _context.TransferOrders.FirstOrDefaultAsync(x => x.transferOrderId == transferIn.transferOrderId);
            if (to == null)
            {
                TempData["StatusMessage"] = "Error. Không tìm thấy Phiếu điều chuyển.";
                return RedirectToAction(nameof(Create));
            }
            transferIn.warehouseIdFrom = to.warehouseIdFrom;
            transferIn.warehouseIdTo = to.warehouseIdTo;
            transferIn.warehouseFrom = await _context.Warehouses.Include(x => x.branch).SingleOrDefaultAsync(x => x.warehouseId == transferIn.warehouseIdFrom);
            transferIn.branchFrom = transferIn.warehouseFrom?.branch;
            transferIn.branchIdFrom = transferIn.warehouseFrom?.branchId;
            transferIn.warehouseTo = await _context.Warehouses.Include(x => x.branch).SingleOrDefaultAsync(x => x.warehouseId == transferIn.warehouseIdTo);
            transferIn.branchTo = transferIn.warehouseTo?.branch;
            transferIn.branchIdTo = transferIn.warehouseTo?.branchId;

            ModelState.Remove("warehouseIdFrom");
            ModelState.Remove("warehouseIdTo");
            ModelState.Remove("branchIdFrom");
            ModelState.Remove("branchIdTo");

            if (ModelState.IsValid)
            {

                //check transfer order
                TransferIn check = await _context.TransferIns.SingleOrDefaultAsync(x => x.transferOrderId.Equals(transferIn.transferOrderId));
                if (check != null)
                {
                    ViewData["StatusMessage"] = "Error. Transfer order already received. " + check.transferInNumber;

                    ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber");
                    ViewData["branchIdFrom"] = new SelectList(_context.Branches, "branchId", "branchName");
                    ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
                    ViewData["branchIdTo"] = new SelectList(_context.Branches, "branchId", "branchName");
                    ViewData["warehouseIdTo"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");


                    return View(transferIn);
                }

                to.isReceived = true;
                to.transferOrderStatus = TransferOrderStatus.Completed;

                try
                {
                    _context.Add(transferIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Không thể tạo phiếu nhập điều chuyển: {ex.InnerException?.Message ?? ex.Message}");
                    ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber", transferIn.transferOrderId);
                    return View(transferIn);
                }

                //auto create transfer in line, full shipment
                List<TransferOrderLine> lines = new List<TransferOrderLine>();
                lines = _context.TransferOrderLines.Include(x => x.product).Where(x => x.transferOrderId.Equals(transferIn.transferOrderId)).ToList();
                foreach (var item in lines)
                {
                    TransferInLine line = new TransferInLine();
                    line.transferIn = transferIn;
                    line.product = item.product;
                    line.qty = item.qty;
                    line.qtyInventory = line.qty * 1;

                    _context.TransferInLines.Add(line);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Details), new { id = transferIn.transferInId });
            }
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber", transferIn.transferOrderId);
            return View(transferIn);
        }

        // GET: TransferIn/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferIn = await _context.TransferIns
                .Include(x => x.branchFrom)
                .Include(x => x.branchTo)
                .Include(x => x.warehouseFrom)
                .Include(x => x.warehouseTo)
                .SingleOrDefaultAsync(m => m.transferInId == id);
            if (transferIn == null)
            {
                return NotFound();
            }
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber", transferIn.transferOrderId);
            ViewData["branchIdFrom"] = new SelectList(_context.Branches, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branches, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
            return View(transferIn);
        }

        // POST: TransferIn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("transferInId,transferOrderId,transferInNumber,transferInDate,description,branchIdFrom,warehouseIdFrom,branchIdTo,warehouseIdTo,HasChild,createdAt")] TransferIn transferIn)
        {
            if (id != transferIn.transferInId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferInExists(transferIn.transferInId))
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
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber", transferIn.transferOrderId);
            ViewData["branchIdFrom"] = new SelectList(_context.Branches, "branchId", "branchName");
            ViewData["warehouseIdFrom"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
            ViewData["branchIdTo"] = new SelectList(_context.Branches, "branchId", "branchName");
            ViewData["warehouseIdTo"] = new SelectList(_context.Warehouses, "warehouseId", "warehouseName");
            return View(transferIn);
        }

        // GET: TransferIn/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferIn = await _context.TransferIns
                    .Include(x => x.branchFrom)
                    .Include(x => x.branchTo)
                    .Include(x => x.warehouseFrom)
                    .Include(x => x.warehouseTo)
                    .Include(t => t.transferOrder)
                    .SingleOrDefaultAsync(m => m.transferInId == id);
            if (transferIn == null)
            {
                return NotFound();
            }

            return View(transferIn);
        }




        // POST: TransferIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transferIn = await _context.TransferIns
                .Include(x => x.transferInLine)
                .Include(x => x.transferOrder)
                .SingleOrDefaultAsync(m => m.transferInId == id);
            try
            {
                _context.TransferInLines.RemoveRange(transferIn.transferInLine);
                _context.TransferIns.Remove(transferIn);
                transferIn.transferOrder.transferOrderStatus = TransferOrderStatus.Open;
                transferIn.transferOrder.isReceived = false;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Error. Calm Down ^_^ and please contact your SysAdmin with this message: " + ex;
                return View(transferIn);
            }

        }

        private bool TransferInExists(string id)
        {
            return _context.TransferIns.Any(e => e.transferInId == id);
        }

    }
}


namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class TransferIn
        {
            public const string Controller = "TransferIn";
            public const string Action = "Index";
            public const string Role = "TransferIn";
            public const string Url = "/TransferIn/Index";
            public const string Name = "TransferIn";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "TransferIn")]
        public bool TransferInRole { get; set; } = false;
    }
}
