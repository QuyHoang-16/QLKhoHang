using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
{
    [Authorize(Roles = "SalesOrderLine")]
    public class SalesOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesOrderLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalesOrderLines.Include(s => s.product).Include(s => s.salesOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalesOrderLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLines
                    .Include(s => s.product)
                    .Include(s => s.salesOrder)
                        .SingleOrDefaultAsync(m => m.salesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            return View(salesOrderLine);
        }


        // GET: SalesOrderLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.SalesOrderLines.SingleOrDefault(m => m.salesOrderLineId == id);
            var selected = _context.SalesOrders.SingleOrDefault(m => m.salesOrderId == masterid);
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode");
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrders, "salesOrderId", "salesOrderNumber");
            if (check == null)
            {
                SalesOrderLine objline = new SalesOrderLine();
                objline.salesOrder = selected;
                objline.salesOrderId = masterid;
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }




        // POST: SalesOrderLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("salesOrderLineId,salesOrderId,productId,qty,price,discountAmount,totalAmount,createdAt")] SalesOrderLine salesOrderLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode", salesOrderLine.productId);
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrders, "salesOrderId", "salesOrderNumber", salesOrderLine.salesOrderId);
            return View(salesOrderLine);
        }

        // GET: SalesOrderLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLines.SingleOrDefaultAsync(m => m.salesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode", salesOrderLine.productId);
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrders, "salesOrderId", "salesOrderNumber", salesOrderLine.salesOrderId);
            return View(salesOrderLine);
        }

        // POST: SalesOrderLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("salesOrderLineId,salesOrderId,productId,qty,price,discountAmount,totalAmount,createdAt")] SalesOrderLine salesOrderLine)
        {
            if (id != salesOrderLine.salesOrderLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderLineExists(salesOrderLine.salesOrderLineId))
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
            ViewData["productId"] = new SelectList(_context.Products, "productId", "productCode", salesOrderLine.productId);
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrders, "salesOrderId", "salesOrderNumber", salesOrderLine.salesOrderId);
            return View(salesOrderLine);
        }

        // GET: SalesOrderLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLines
                    .Include(s => s.product)
                    .Include(s => s.salesOrder)
                    .SingleOrDefaultAsync(m => m.salesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            return View(salesOrderLine);
        }




        // POST: SalesOrderLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var salesOrderLine = await _context.SalesOrderLines.SingleOrDefaultAsync(m => m.salesOrderLineId == id);
            _context.SalesOrderLines.Remove(salesOrderLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderLineExists(string id)
        {
            return _context.SalesOrderLines.Any(e => e.salesOrderLineId == id);
        }

    }
}

namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class SalesOrderLine
        {
            public const string Controller = "SalesOrderLine";
            public const string Action = "Index";
            public const string Role = "SalesOrderLine";
            public const string Url = "/SalesOrderLine/Index";
            public const string Name = "SalesOrderLine";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "SalesOrderLine")]
        public bool SalesOrderLineRole { get; set; } = false;
    }
}
