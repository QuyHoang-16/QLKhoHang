using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;

namespace QuanLyKho.Controllers.Api
{
    [ApiController]
    [Route("api/CustomerLine")]
    [Authorize(Roles = "Customer,CustomerLine")] 
    public class CustomerLineApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerLineApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string masterid)
        {
            if (string.IsNullOrWhiteSpace(masterid))
            {
                return Ok(new { data = Array.Empty<object>() });
            }

            var rows = await _context.CustomerLines
                .Where(x => x.customerId == masterid)
                .Select(x => new
                {
                    x.customerLineId,
                    x.firstName,
                    x.lastName,
                    x.jobTitle
                })
                .ToListAsync();

            return Ok(new { data = rows });
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerLine payload)
        {
            if (payload == null)
            {
                return BadRequest(new { success = false, message = "Invalid payload" });
            }
            if (string.IsNullOrWhiteSpace(payload.customerLineId))
            {
                payload.customerLineId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(payload.customerId))
            {
                return BadRequest(new { success = false, message = "customerId is required" });
            }

            var exists = await _context.CustomerLines.AnyAsync(x => x.customerLineId == payload.customerLineId);
            if (!exists)
            {
                _context.CustomerLines.Add(payload);
            }
            else
            {
                _context.Entry(payload).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Saved successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var row = await _context.CustomerLines.FindAsync(id);
            if (row == null)
            {
                return NotFound(new { success = false, message = "Not found" });
            }
            _context.CustomerLines.Remove(row);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Deleted" });
        }
    }
}
