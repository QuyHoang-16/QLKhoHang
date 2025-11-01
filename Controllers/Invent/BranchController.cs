using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.Data;
using QuanLyKho.Models.Invent;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Controllers.Invent
{
    [Authorize]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

  
        public async Task<IActionResult> Index()
        {
            return View(await _context.Branches.OrderByDescending(x => x.createdAt).ToListAsync());
        }

   
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                        .SingleOrDefaultAsync(m => m.branchId == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }


     
        public IActionResult Create()
        {
            return View();
        }




      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("branchId,branchName,description,street1,street2,city,province,country,createdAt,isDefaultBranch")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.branchId == id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("branchId,branchName,description,street1,street2,city,province,country,createdAt,isDefaultBranch")] Branch branch)
        {
            if (id != branch.branchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branch);
                    await _context.SaveChangesAsync();

                    if (branch.isDefaultBranch)
                    {
                        List<Branch> others = await _context.Branches.Where(x => !x.branchId.Equals(branch.branchId)).ToListAsync();
                        foreach (var item in others)
                        {
                            item.isDefaultBranch = false;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.branchId))
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
            return View(branch);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                    .SingleOrDefaultAsync(m => m.branchId == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }





        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.branchId == id);
            try
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Error. Calm Down ^_^ and please contact your SysAdmin with this message: " + ex;
                return View(branch);
            }



        }

        private bool BranchExists(string id)
        {
            return _context.Branches.Any(e => e.branchId == id);
        }

    }
}

namespace QuanLyKho.MVC
{
    public static partial class Pages
    {
        public static class Branch
        {
            public const string Controller = "Branch";
            public const string Action = "Index";
            public const string Role = "Branch";
            public const string Url = "/Branch/Index";
            public const string Name = "Branch";
        }
    }
}
namespace QuanLyKho.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Branch")]
        public bool BranchRole { get; set; } = false;
    }
}
