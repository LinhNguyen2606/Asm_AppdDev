using Asm_AppdDev.Models;
using Asm_AppdDev.Utils;
using Asm_AppdDev.ViewModels;
using Microsoft.AspNet.Identity;
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
        public ActionResult IndexTrainee()
        {
            var trainee = _context.Trainees.ToList();
            var user = _context.Users.ToList();
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
    }
}