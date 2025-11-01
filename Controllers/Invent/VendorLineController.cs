using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
{
    [Authorize(Roles = "VendorLine")]
    public class VendorLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorLines.Include(v => v.vendor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorLine = await _context.VendorLines
                    .Include(v => v.vendor)
                        .SingleOrDefaultAsync(m => m.vendorLineId == id);
            if (vendorLine == null)
            {
                return NotFound();
            }

            return View(vendorLine);
        }


        // GET: VendorLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.VendorLines.SingleOrDefault(m => m.vendorLineId == id);
            var selected = _context.Vendors.SingleOrDefault(m => m.vendorId == masterid);
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorId");
            if (check == null)
            {
                VendorLine objline = new VendorLine();
                objline.vendor = selected;
                objline.vendorId = masterid;
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }




        // POST: VendorLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("vendorLineId,jobTitle,vendorId,firstName,lastName,middleName,nickName,gender,salutation,mobilePhone,officePhone,fax,personalEmail,workEmail,createdAt")] VendorLine vendorLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorId", vendorLine.vendorId);
            return View(vendorLine);
        }

        // GET: VendorLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorLine = await _context.VendorLines.SingleOrDefaultAsync(m => m.vendorLineId == id);
            if (vendorLine == null)
            {
                return NotFound();
            }
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorId", vendorLine.vendorId);
            return View(vendorLine);
        }

        // POST: VendorLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("vendorLineId,jobTitle,vendorId,firstName,lastName,middleName,nickName,gender,salutation,mobilePhone,officePhone,fax,personalEmail,workEmail,createdAt")] VendorLine vendorLine)
        {
            if (id != vendorLine.vendorLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorLineExists(vendorLine.vendorLineId))
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
            ViewData["vendorId"] = new SelectList(_context.Vendors, "vendorId", "vendorId", vendorLine.vendorId);
            return View(vendorLine);
        }

        // GET: VendorLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorLine = await _context.VendorLines
                    .Include(v => v.vendor)
                    .SingleOrDefaultAsync(m => m.vendorLineId == id);
            if (vendorLine == null)
            {
                return NotFound();
            }

            return View(vendorLine);
        }




        // POST: VendorLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vendorLine = await _context.VendorLines.SingleOrDefaultAsync(m => m.vendorLineId == id);
            _context.VendorLines.Remove(vendorLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorLineExists(string id)
        {
            return _context.VendorLines.Any(e => e.vendorLineId == id);
        }

    }
}


namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class VendorLine
        {
            public const string Controller = "VendorLine";
            public const string Action = "Index";
            public const string Role = "VendorLine";
            public const string Url = "/VendorLine/Index";
            public const string Name = "VendorLine";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "VendorLine")]
        public bool VendorLineRole { get; set; } = false;
    }
}
