using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/VendorLine")]
    [Authorize(Roles = "Vendor,VendorLine")] // allow either role to access the API from Vendor details page
    public class VendorLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendorLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VendorLine?masterid={vendorId}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.VendorLines
                .Where(x => x.vendorId == masterid)
                .Select(x => new {
                    x.vendorLineId,
                    x.firstName,
                    x.lastName,
                    x.jobTitle
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }

        // POST: api/VendorLine
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VendorLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }

            if (string.IsNullOrWhiteSpace(payload.vendorLineId))
            {
                payload.vendorLineId = Guid.NewGuid().ToString();
            }

            // Basic validation
            if (string.IsNullOrWhiteSpace(payload.vendorId))
            {
                return BadRequest(new { success = false, message = "vendorId is required" });
            }

            var exists = await _context.VendorLines.AnyAsync(x => x.vendorLineId == payload.vendorLineId);
            if (!exists)
            {
                _context.VendorLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        // DELETE: api/VendorLine/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.VendorLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }

            _context.VendorLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
