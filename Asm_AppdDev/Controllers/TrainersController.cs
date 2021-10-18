using Asm_AppdDev.Models;
using Asm_AppdDev.Utils;
using Asm_AppdDev.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asm_AppdDev.Controllers
{
    [Authorize(Roles = Role.Trainer)]
    public class TrainersController : Controller
    {
        private ApplicationDbContext _context;
        public TrainersController()
        {
            _context = new ApplicationDbContext();
        }
        
        // GET: Trainers
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == userId);
            return View(trainerInDb);
        }

        [HttpGet]
        public ActionResult EditTrainerAccount(int id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.Id == id);
            if(trainerInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInDb);
        }

        [HttpPost]
        public ActionResult EditTrainerAccount(Trainer trainer)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(t => t.Id == trainer.Id);
            if (trainerInDb == null)
            {
                return HttpNotFound();
            }
            trainerInDb.Name = trainer.Name;
            trainerInDb.Age = trainer.Age;
            trainerInDb.Address = trainer.Address;
            trainerInDb.Specialty = trainer.Specialty;
            _context.SaveChanges();
            return RedirectToAction("Index", "Trainers");
        }
        [HttpGet]
    }
}