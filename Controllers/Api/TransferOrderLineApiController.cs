using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/TransferOrderLine")]
    [Authorize(Roles = "TransferOrder,TransferOrderLine")] // page roles
    public class TransferOrderLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransferOrderLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransferOrderLine?masterid={transferOrderId}
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.TransferOrderLines
                .Where(x => x.transferOrderId == masterid)
                .Include(x => x.product)
                .Select(x => new
                {
                    x.transferOrderLineId,
                    x.qty,
                    product = new { productCode = x.product != null ? x.product.productCode : string.Empty }
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/TransferOrderLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransferOrderLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }
            if (string.IsNullOrWhiteSpace(payload.transferOrderLineId))
            {
                payload.transferOrderLineId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(payload.transferOrderId) || string.IsNullOrWhiteSpace(payload.productId))
            {
                return BadRequest(new { success = false, message = "transferOrderId and productId are required" });
            }

            var exists = await _context.TransferOrderLines.AnyAsync(x => x.transferOrderLineId == payload.transferOrderLineId);
            if (!exists)
            {
                _context.TransferOrderLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/TransferOrderLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.TransferOrderLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }
            _context.TransferOrderLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
