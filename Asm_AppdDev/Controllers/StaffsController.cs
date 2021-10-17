using Asm_AppdDev.Models;
using Asm_AppdDev.Utils;
using Asm_AppdDev.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Asm_AppdDev.Controllers
{
    public class StaffsController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        public StaffsController()
        {
            _context = new ApplicationDbContext();
        }
        public StaffsController(ApplicationUserManager userManager)
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
        [Authorize(Roles = "staff")]
        public ActionResult IndexTrainee(string searchString)
        {
            var trainee = _context.Trainees.ToList();
            var user = _context.Users.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                trainee = trainee.Where(
                                 s => s.Name.ToLower().Contains(searchString.ToLower()) ||
                                 s.Age.ToString().Contains(searchString.ToLower())).ToList();
            }
            return View(trainee);
        }


        [Authorize(Roles = "staff")]
        [HttpGet]
        public ActionResult CreateTraineeAccount()
        {
            return View();
        }

        [Authorize(Roles = "staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTraineeAccount(TraineeAccountViewModels viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var traineeId = user.Id;
                var newTrainee = new Trainee()
                {
                    TraineeId = traineeId,
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    DateOfBirth = viewModel.DateOfBirth,
                    Address = viewModel.Address,
                    Education = viewModel.Education
                };

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Trainee);
                    _context.Trainees.Add(newTrainee);
                    _context.SaveChanges();
                    return RedirectToAction("IndexTrainee", "Staffs");
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
        [Authorize(Roles = "staff")]
        [HttpGet]
        public ActionResult EditTraineeAccount(int id)
        {
            var traineeInDb = _context.Trainees.SingleOrDefault(t => t.Id == id);
                if(traineeInDb == null)
                {
                    return HttpNotFound();
                }
            return View(traineeInDb);
        }
        [Authorize(Roles = "staff")]
        [HttpPost]
        public ActionResult EditTraineeAccount(Trainee trainee)
        {
            if(!ModelState.IsValid)
            {
                return View(trainee);
            }
            var traineeInfoDb = _context.Trainees.SingleOrDefault(t => t.Id == trainee.Id);

            if(traineeInfoDb == null)
            {
                return HttpNotFound();
            }
            traineeInfoDb.Name = trainee.Name;
            traineeInfoDb.Age = trainee.Age;
            traineeInfoDb.DateOfBirth = trainee.DateOfBirth;
            traineeInfoDb.Address = trainee.Address;
            traineeInfoDb.Education = trainee.Education;

            _context.SaveChanges();
            return RedirectToAction("IndexTrainee", "Staffs");
        }

        [Authorize(Roles = "staff")]
        [HttpGet]
        public ActionResult DeleteTraineeAccount(string id)
        {
            var traineeInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            var traineeInFoDb = _context.Trainees.SingleOrDefault(i => i.TraineeId == id);
            if(traineeInDb == null || traineeInFoDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(traineeInDb);
            _context.Trainees.Remove(traineeInFoDb);
            _context.SaveChanges();
            return RedirectToAction("IndexTrainee", "Staffs");
        }
        [Authorize(Roles = "staff")]
        [HttpGet]
        public ActionResult TraineeInfoDetails(string id)
        {
            var traineeId = User.Identity.GetUserId();

            var traineeInfoDb = _context.Trainees.SingleOrDefault(t => t.TraineeId == id);
            if(traineeInfoDb == null)
            {
                return HttpNotFound();
            }
            return View(traineeInfoDb);
        }

        [Authorize(Roles = "staff")]
        public ActionResult TraineePasswordReset(string id)
        {
            var traineeInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = traineeInDb.Id;
            if (userId != null)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                string newPassWord = "Password123@";
                userManager.AddPassword(userId, newPassWord);
            }
            _context.SaveChanges();
            return RedirectToAction("IndexTrainee", "Staffs");

        }
    }
}