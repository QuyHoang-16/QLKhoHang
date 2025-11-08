using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/ReceivingLine")]
    [Authorize(Roles = "Receiving,ReceivingLine")] // allow from Receiving pages
    public class ReceivingLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReceivingLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceivingLine?masterid={receivingId}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.ReceivingLines
                .Where(x => x.receivingId == masterid)
                .Include(x => x.product)
                .Select(x => new
                {
                    x.receivingLineId,
                    x.qty,
                    x.qtyReceive,
                    product = new { productCode = x.product != null ? x.product.productCode : string.Empty }
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/ReceivingLine (not used by current UI, provided for parity)
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReceivingLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }

            if (string.IsNullOrWhiteSpace(payload.receivingLineId))
            {
                payload.receivingLineId = Guid.NewGuid().ToString();
            }

            if (string.IsNullOrWhiteSpace(payload.receivingId) || string.IsNullOrWhiteSpace(payload.productId))
            {
                return BadRequest(new { success = false, message = "receivingId and productId are required" });
            }

            // compute inventory qty if missing
            if (payload.qtyInventory == 0)
            {
                payload.qtyInventory = payload.qtyReceive * 1;
            }

            var exists = await _context.ReceivingLines.AnyAsync(x => x.receivingLineId == payload.receivingLineId);
            if (!exists)
            {
                _context.ReceivingLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/ReceivingLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.ReceivingLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }

            _context.ReceivingLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
