using Asm_AppdDev.Models;
using Asm_AppdDev.Utils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asm_AppdDev.Controllers
{
    [Authorize(Roles = Role.Trainee)]
    public class TraineesController : Controller
    {
        private ApplicationDbContext _context;
        public TraineesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Trainee
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var traineeInDb = _context.Trainees
                .SingleOrDefault(t => t.TraineeId == userId);
            return View(traineeInDb);
        }

        [HttpGet]
        public ActionResult ViewCourses()
        {
            var userId = User.Identity.GetUserId();
            var category = _context.Categories.ToList();
            var courses = _context.TraineesToCourses
                .Where(t => t.Trainee.TraineeId == userId)
                .Select(t => t.Course)
                .ToList();
            return View(courses);
        }

        [HttpGet]
        public ActionResult CourseTrainees(int id)
        {
            var traineesCourse = _context.TraineesToCourses
                .Where(t => t.CourseId == id)
                .Select(t => t.Trainee)
                .ToList();
            return View(traineesCourse);
        }
    }
}