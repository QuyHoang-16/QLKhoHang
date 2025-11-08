using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/PurchaseOrderLine")]
    [Authorize(Roles = "PurchaseOrder,PurchaseOrderLine")] // allow PO pages to call this API
    public class PurchaseOrderLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrderLine?masterid={purchaseOrderId}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.PurchaseOrderLines
                .Where(x => x.purchaseOrderId == masterid)
                .Include(x => x.product)
                .Select(x => new
                {
                    x.purchaseOrderLineId,
                    x.qty,
                    x.price,
                    product = new { productCode = x.product != null ? x.product.productCode : string.Empty }
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/PurchaseOrderLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseOrderLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }

            if (string.IsNullOrWhiteSpace(payload.purchaseOrderLineId))
            {
                payload.purchaseOrderLineId = Guid.NewGuid().ToString();
            }

            if (string.IsNullOrWhiteSpace(payload.purchaseOrderId) || string.IsNullOrWhiteSpace(payload.productId))
            {
                return BadRequest(new { success = false, message = "purchaseOrderId and productId are required" });
            }

            // compute totalAmount if not set
            if (payload.totalAmount == 0m)
            {
                payload.totalAmount = (decimal)payload.qty * payload.price - payload.discountAmount;
            }

            var exists = await _context.PurchaseOrderLines.AnyAsync(x => x.purchaseOrderLineId == payload.purchaseOrderLineId);
            if (!exists)
            {
                _context.PurchaseOrderLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/PurchaseOrderLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.PurchaseOrderLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }

            _context.PurchaseOrderLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
