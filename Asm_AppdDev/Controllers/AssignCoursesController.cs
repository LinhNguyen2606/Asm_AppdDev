using Asm_AppdDev.Models;
using Asm_AppdDev.Utils;
using Asm_AppdDev.ViewModels;
using Asm_AppdDev.ViewModels.TraineesCourses;
using Asm_AppdDev.ViewModels.TrainersCourses;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Asm_AppdDev.Controllers
{
    public class AssignCoursesController : Controller
    {
        private ApplicationDbContext _context;
        public AssignCoursesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: AssignCourses
        [HttpGet]
        public ActionResult GetTrainers(string searchString)
        {
            var courses = _context.Courses.Include(t => t.Category).ToList();
            var trainer = _context.TrainersToCourses.ToList();

            List<TrainersInCoursesViewModels> viewModel = _context.TrainersToCourses
                .GroupBy(i => i.Course)
                .Select(res => new TrainersInCoursesViewModels
                {
                    Course = res.Key,
                    Trainers = res.Select(u => u.Trainer).ToList()
                })
                .ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                viewModel = viewModel
                    .Where(t => t.Course.Name.ToLower().Contains(searchString.ToLower()))
                    .ToList();
            }
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult AddTrainer()
        {
            var viewModel = new TrainersToCoursesViewModels
            {
                Courses = _context.Courses.ToList(),
                Trainers = _context.Trainers.ToList(),
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddTrainer(TrainersToCoursesViewModels model)
        {
            var getViewModel = new TrainersToCoursesViewModels
            {
                Courses = _context.Courses.ToList(),
                Trainers = _context.Trainers.ToList(),
            };

            if(!ModelState.IsValid)
            {
                return View(getViewModel);
            }

            var viewModel = new TrainersToCourses
            {
                CourseId = model.CourseId,
                TrainerId = model.TrainerId
            };

            List<TrainersToCourses> trainersToCourses = _context.TrainersToCourses.ToList();
            bool alreadyExist = trainersToCourses.Any(item => item.CourseId == model.CourseId && item.TrainerId == model.TrainerId);
            if (alreadyExist == true)
            {
                ModelState.AddModelError("", "Trainer is Already Exist");
                return View(getViewModel);
            }
            _context.TrainersToCourses.Add(viewModel);
            _context.SaveChanges();
            return RedirectToAction("GetTrainers", "AssignCourses");
        }
        [HttpGet]
        public ActionResult RemoveTrainer()
        {
            var getTrainer = _context.TrainersToCourses.Select(t => t.Trainer)
                .Distinct()
                .ToList();
            var getCourse = _context.TrainersToCourses.Select(t => t.Course)
                .Distinct()
                .ToList();
            var viewModel = new TrainersToCoursesViewModels
            {
                Courses = getCourse,
                Trainers = getTrainer,
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult RemoveTrainer(TrainersToCoursesViewModels models)
        {
            var getTrainer = _context.TrainersToCourses
                .SingleOrDefault(c => c.CourseId == models.CourseId && c.TrainerId == models.TrainerId);
            if(getTrainer == null)
            {
                return RedirectToAction("GetTrainers", "AssignCourses");
            }

            _context.TrainersToCourses.Remove(getTrainer);
            _context.SaveChanges();
            return RedirectToAction("GetTrainers", "AssignCourses");
        }
        //Trainees
        [HttpGet]
        public ActionResult GetTrainees(string searchString)
        {
            var courses = _context.Courses.Include(t => t.Category).ToList();
            var trainees = _context.TraineesToCourses.ToList();

            List<TraineesInCoursesViewModels> viewModel = _context.TraineesToCourses
                .GroupBy(i => i.Course)
                .Select(res => new TraineesInCoursesViewModels
                {
                    Course = res.Key,
                    Trainees = res.Select(u => u.Trainee).ToList(),
                })
                .ToList();

            if(!string.IsNullOrEmpty(searchString))
            {
                viewModel = viewModel
                   .Where(t => t.Course.Name
                   .ToLower()
                   .Contains(searchString.ToLower()))
                   .ToList();
            }

            return View(viewModel);
        }
        [HttpGet]
        public ActionResult AddTrainee()
        {
            var viewModel = new TraineesToCoursesViewModels
            {
                Courses = _context.Courses.ToList(),
                Trainees = _context.Trainees.ToList(),
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddTrainee(TraineesToCoursesViewModels model)
        {
            //get view if model is invalid
            var getViewModel = new TraineesToCourses
            {
                CourseId = model.CourseId,
                TraineeId = model.TraineeId
            };
            List<TraineesToCourses> traineesCourses = _context.TraineesToCourses.ToList();
            bool alreadyExist = traineesCourses.Any(item => item.CourseId == model.CourseId && item.TraineeId == model.TraineeId);
            if (alreadyExist == true)
            {
                ModelState.AddModelError("", "Trainee is already assignned this Course");
                return RedirectToAction("GetTrainees", "AssignCourses");
            }
            _context.TraineesToCourses.Add(getViewModel);
            _context.SaveChanges();

            return RedirectToAction("GetTrainees", "AssignCourses");    

        }
    }
}