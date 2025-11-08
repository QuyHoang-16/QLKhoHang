using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/SalesOrderLine")]
    [Authorize(Roles = "SalesOrder,SalesOrderLine")] // allow SO pages to call this API
    public class SalesOrderLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesOrderLine?masterid={salesOrderId}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.SalesOrderLines
                .Where(x => x.salesOrderId == masterid)
                .Include(x => x.product)
                .Select(x => new
                {
                    x.salesOrderLineId,
                    x.qty,
                    x.price,
                    product = new { productCode = x.product != null ? x.product.productCode : string.Empty }
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/SalesOrderLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesOrderLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }
            if (string.IsNullOrWhiteSpace(payload.salesOrderLineId))
            {
                payload.salesOrderLineId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(payload.salesOrderId) || string.IsNullOrWhiteSpace(payload.productId))
            {
                return BadRequest(new { success = false, message = "salesOrderId and productId are required" });
            }
            if (payload.totalAmount == 0m)
            {
                payload.totalAmount = (decimal)payload.qty * payload.price - payload.discountAmount;
            }

            var exists = await _context.SalesOrderLines.AnyAsync(x => x.salesOrderLineId == payload.salesOrderLineId);
            if (!exists)
            {
                _context.SalesOrderLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/SalesOrderLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.SalesOrderLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }
            _context.SalesOrderLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
