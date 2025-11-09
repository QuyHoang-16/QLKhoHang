using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/TransferOutLine")]
    [Authorize(Roles = "TransferOut,TransferOutLine")] // page roles
    public class TransferOutLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransferOutLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransferOutLine?masterid={transferOutId}
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.TransferOutLines
                .Where(x => x.transferOutId == masterid)
                .Include(x => x.product)
                .Select(x => new
                {
                    x.transferOutLineId,
                    x.qty,
                    product = new { productCode = x.product != null ? x.product.productCode : string.Empty }
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/TransferOutLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransferOutLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }
            if (string.IsNullOrWhiteSpace(payload.transferOutLineId))
            {
                payload.transferOutLineId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(payload.transferOutId) || string.IsNullOrWhiteSpace(payload.productId))
            {
                return BadRequest(new { success = false, message = "transferOutId and productId are required" });
            }

            var exists = await _context.TransferOutLines.AnyAsync(x => x.transferOutLineId == payload.transferOutLineId);
            if (!exists)
            {
                _context.TransferOutLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/TransferOutLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.TransferOutLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }
            _context.TransferOutLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
