using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
{
    [Authorize(Roles = "TransferOrderLine")]
    public class TransferOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransferOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransferOrderLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TransferOrderLines.Include(t => t.product).Include(t => t.transferOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TransferOrderLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOrderLine = await _context.TransferOrderLines
                    .Include(t => t.product)
                    .Include(t => t.transferOrder)
                        .SingleOrDefaultAsync(m => m.transferOrderLineId == id);
            if (transferOrderLine == null)
            {
                return NotFound();
            }

            return View(transferOrderLine);
        }


        // GET: TransferOrderLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.TransferOrderLines.SingleOrDefault(m => m.transferOrderLineId == id);
            var selected = _context.TransferOrders.SingleOrDefault(m => m.transferOrderId == masterid);
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode");
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber");
            if (check == null)
            {
                TransferOrderLine objline = new TransferOrderLine();
                objline.transferOrder = selected;
                objline.transferOrderId = masterid;
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }




        // POST: TransferOrderLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transferOrderLineId,transferOrderId,productId,qty,createdAt")] TransferOrderLine transferOrderLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transferOrderLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode", transferOrderLine.productId);
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber", transferOrderLine.transferOrderId);
            return View(transferOrderLine);
        }

        // GET: TransferOrderLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOrderLine = await _context.TransferOrderLines.SingleOrDefaultAsync(m => m.transferOrderLineId == id);
            if (transferOrderLine == null)
            {
                return NotFound();
            }
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode", transferOrderLine.productId);
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber", transferOrderLine.transferOrderId);
            return View(transferOrderLine);
        }

        // POST: TransferOrderLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("transferOrderLineId,transferOrderId,productId,qty,createdAt")] TransferOrderLine transferOrderLine)
        {
            if (id != transferOrderLine.transferOrderLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferOrderLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferOrderLineExists(transferOrderLine.transferOrderLineId))
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
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode", transferOrderLine.productId);
            ViewData["transferOrderId"] = new SelectList(_context.TransferOrders, "transferOrderId", "transferOrderNumber", transferOrderLine.transferOrderId);
            return View(transferOrderLine);
        }

        // GET: TransferOrderLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferOrderLine = await _context.TransferOrderLines
                    .Include(t => t.product)
                    .Include(t => t.transferOrder)
                    .SingleOrDefaultAsync(m => m.transferOrderLineId == id);
            if (transferOrderLine == null)
            {
                return NotFound();
            }

            return View(transferOrderLine);
        }




        // POST: TransferOrderLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transferOrderLine = await _context.TransferOrderLines.SingleOrDefaultAsync(m => m.transferOrderLineId == id);
            _context.TransferOrderLines.Remove(transferOrderLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferOrderLineExists(string id)
        {
            return _context.TransferOrderLines.Any(e => e.transferOrderLineId == id);
        }

    }
}


namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class TransferOrderLine
        {
            public const string Controller = "TransferOrderLine";
            public const string Action = "Index";
            public const string Role = "TransferOrderLine";
            public const string Url = "/TransferOrderLine/Index";
            public const string Name = "TransferOrderLine";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "TransferOrderLine")]
        public bool TransferOrderLineRole { get; set; } = false;
    }
}
