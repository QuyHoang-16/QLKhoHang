using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/TransferInLine")]
    [Authorize(Roles = "TransferIn,TransferInLine")] // page roles
    public class TransferInLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransferInLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransferInLine?masterid={transferInId}
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.TransferInLines
                .Where(x => x.transferInId == masterid)
                .Include(x => x.product)
                .Select(x => new
                {
                    x.transferInLineId,
                    x.qty,
                    product = new { productCode = x.product != null ? x.product.productCode : string.Empty }
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/TransferInLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransferInLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }
            if (string.IsNullOrWhiteSpace(payload.transferInLineId))
            {
                payload.transferInLineId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(payload.transferInId) || string.IsNullOrWhiteSpace(payload.productId))
            {
                return BadRequest(new { success = false, message = "transferInId and productId are required" });
            }

            var exists = await _context.TransferInLines.AnyAsync(x => x.transferInLineId == payload.transferInLineId);
            if (!exists)
            {
                _context.TransferInLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/TransferInLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.TransferInLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }
            _context.TransferInLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
