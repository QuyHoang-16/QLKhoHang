using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/ShipmentLine")]
    [Authorize(Roles = "Shipment,ShipmentLine")] // allow Shipment pages to call this API
    public class ShipmentLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShipmentLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ShipmentLine?masterid={shipmentId}
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.ShipmentLines
                .Where(x => x.shipmentId == masterid)
                .Include(x => x.product)
                .Select(x => new
                {
                    x.shipmentLineId,
                    x.qty,
                    x.qtyShipment,
                    product = new { productCode = x.product != null ? x.product.productCode : string.Empty }
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/ShipmentLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShipmentLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }
            if (string.IsNullOrWhiteSpace(payload.shipmentLineId))
            {
                payload.shipmentLineId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(payload.shipmentId) || string.IsNullOrWhiteSpace(payload.productId))
            {
                return BadRequest(new { success = false, message = "shipmentId and productId are required" });
            }
            if (payload.qtyInventory == 0)
            {
                payload.qtyInventory = payload.qtyShipment * -1;
            }

            var exists = await _context.ShipmentLines.AnyAsync(x => x.shipmentLineId == payload.shipmentLineId);
            if (!exists)
            {
                _context.ShipmentLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/ShipmentLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.ShipmentLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }
            _context.ShipmentLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
