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
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteStaffAccount(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            var staffInfoDb = _context.Staffs.SingleOrDefault(i => i.StaffId == id);
            if (staffInDb == null || staffInfoDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(staffInDb);
            _context.Staffs.Remove(staffInfoDb);
            _context.SaveChanges();
            return RedirectToAction("IndexStaff", "Admins");
        }

        [Authorize(Roles = "admin")]
        public ActionResult StaffPasswordReset(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            if (staffInDb  == null)
            {
                return HttpNotFound();
            }
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = staffInDb.Id;
            if (userId != null)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                string newPassword = "Password123@";
                userManager.AddPassword(userId, newPassword);
            }
            _context.SaveChanges();
            return RedirectToAction("IndexStaff", "Admins");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult IndexTrainer()
        {
            var trainer = _context.Trainers.ToList();
            var user = _context.Users.ToList();
            return View(trainer);
        }

        [HttpGet]
        public ActionResult CreateTrainerAccount()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTrainerAccount(TrainerAccountViewModels viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser{ UserName = viewModel.Email, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var trainerId = user.Id;
                var newTrainer = new Trainer()
                {
                    TrainerId = trainerId,
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    Address = viewModel.Address,
                    Specialty = viewModel.Specialty
                };

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Trainer);
                    _context.Trainers.Add(newTrainer);
                    _context.SaveChanges();
                    return RedirectToAction("IndexTrainer", "Admins");
                }
                AddErrors(result);
            }
            return View(viewModel);
 
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditTrainerAccount(int id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(u => u.Id == id);
            if(trainerInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInDb);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditTrainerAccount(Trainer trainer)
        {
            if(!ModelState.IsValid)
            {
                return View(trainer);
            }
            var trainerInfoDb = _context.Trainers.SingleOrDefault(u => u.Id == trainer.Id);

            if(trainerInfoDb == null)
            {
                return HttpNotFound();
            }
            trainerInfoDb.Name = trainer.Name;
            trainerInfoDb.Age = trainer.Age;
            trainerInfoDb.Address = trainer.Address;
            trainerInfoDb.Specialty = trainer.Specialty;

            _context.SaveChanges();
            return RedirectToAction("IndexTrainer", "Admins");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteTrainerAccount(string id)
        {
            var trainerInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            var trainerInFoDb = _context.Trainers.SingleOrDefault(i => i.TrainerId == id);
            if(trainerInDb == null || trainerInFoDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(trainerInDb);
            _context.Trainers.Remove(trainerInFoDb);
            _context.SaveChanges();
            return RedirectToAction("IndexTrainer", "Admins");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult TrainerInfoDetails(string id)
        {
            var trainerId = User.Identity.GetUserId();

            var trainerInfoInDb = _context.Trainers
                .SingleOrDefault(t => t.TrainerId == id);

            if(trainerInfoInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInfoInDb);
        }

        
    }
}