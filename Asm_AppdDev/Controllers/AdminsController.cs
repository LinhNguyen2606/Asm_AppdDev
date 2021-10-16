using Asm_AppdDev.Models;
using Asm_AppdDev.Utils;
using Asm_AppdDev.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Asm_AppdDev.Controllers
{
    public class AdminsController : Controller
    {
        //tao ket noi
        private ApplicationDbContext _context;

        private ApplicationUserManager _userManager;

        public AdminsController()
        {
            _context = new ApplicationDbContext();
        }

        public AdminsController(ApplicationUserManager userManager)
        {
            _context = new ApplicationDbContext();
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult IndexStaff()
        {
            var staff = _context.Staffs.ToList();
            var user = _context.Users.ToList();
            return View(staff);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateStaffAccount()
        {
            return View();
        }
        [Authorize(Roles = "admin")]    
        [HttpPost]
        public async Task<ActionResult> CreateStaffAccount(StaffAccountViewModels viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser{ UserName = viewModel.Email, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var staffId = user.Id;
                var newStaff = new Staff()
                {
                    StaffId = staffId,
                    FullName = viewModel.FullName,
                    Age = viewModel.Age,
                    Address = viewModel.Address
                };

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Staff);
                    _context.Staffs.Add(newStaff);
                    _context.SaveChanges();
                    return RedirectToAction("IndexStaff", "Admins");
                }
                AddErrors(result);
            }
            return View(viewModel);
        }
        

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditStaffAccount(int id)
        {
            var staffInDb = _context.Staffs.SingleOrDefault(t => t.Id == id);
            if (staffInDb == null)
            {
                return HttpNotFound();
            }
            return View(staffInDb);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditStaffAccount(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return View(staff);
            }
            var staffInDb = _context.Staffs.SingleOrDefault(t => t.Id == staff.Id);
            if (staffInDb == null)
            {
                return HttpNotFound();
            }

            staffInDb.FullName = staff.FullName;
            staffInDb.Age = staff.Age;
            staffInDb.Address = staff.Address;

            _context.SaveChanges();
            return RedirectToAction("IndexStaff", "Admins");
        }
    }
}