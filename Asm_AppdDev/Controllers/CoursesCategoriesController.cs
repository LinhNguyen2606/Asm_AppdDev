using Asm_AppdDev.Models;
using Asm_AppdDev.Utils;
using Asm_AppdDev.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Asm_AppdDev.Controllers
{
	[Authorize(Roles = Role.Staff)]
	public class CoursesCategoriesController : Controller
	{
		private ApplicationDbContext _context;
		public CoursesCategoriesController()
		{
			_context = new ApplicationDbContext();
		}
		[HttpGet]
		public ActionResult Index(string searchString)
		{
			var courses = _context.Courses.Include(c => c.Category);

			if (!String.IsNullOrEmpty(searchString))
			{
				courses = courses.Where(
								 s => s.Name.Contains(searchString) ||
								 s.Category.Name.Contains(searchString));			
			}
			return View(courses);
		}

		//// GET: View Course Details
		//public ActionResult Details(int? id)
		//{
		//  if (id == null)
		//  {
		//    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//  }
		//  var course = _context.Courses.Find(id);
		//  if (course == null)
		//  {
		//    return HttpNotFound();
		//  }
		//  return View(course);
		//}

		//GET: Create
		[HttpGet]

		public ActionResult Create()
		{
			var viewModel = new CourseCategoriesViewModel
			{
				Categories = _context.Categories.ToList(),
				Course = new Course(),
			};
			return View(viewModel);
		}

		[HttpPost]

		public ActionResult Create(CourseCategoriesViewModel coursecate)
		{
			if (ModelState.IsValid)
			{
				var check = _context.Courses.Include(c => c.Category)
					.Where(c => c.Name == coursecate.Course.Name && c.CategoryId == coursecate.Course.CategoryId);
				//GET NameCOurse and Category ID from VM
				if (check.Count() > 0) //list ID comparison, if count == 0. jump to else
				{
					ModelState.AddModelError("", "Course Already Exists.");
				}
				else
				{
					_context.Courses.Add(coursecate.Course);
					_context.SaveChanges();
					return RedirectToAction("Index");
				}
			}

			var courseVM = new CourseCategoriesViewModel()
			{
				Categories = _context.Categories.ToList(),
				Course = coursecate.Course,
			};
			return View(courseVM);
		}
		//GET: Edit
		[HttpGet]

		public ActionResult Edit(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);
			if (courseInDb == null)
			{
				return HttpNotFound();
			}
			var viewModel = new CourseCategoriesViewModel
			{
				Course = courseInDb,
				Categories = _context.Categories.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]

		public ActionResult Edit(CourseCategoriesViewModel edit)
		{
			if (ModelState.IsValid)
			{
				var check = _context.Courses.Include(c => c.Category).Where(c => c.Name == edit.Course.Name && c.CategoryId == edit.Course.CategoryId);

				if (check.Count() > 0)
				{
					ModelState.AddModelError("", "Course Already Exists.");
				}
				else
				{
					var courseInDb = _context.Courses.Find(edit.Course.Id);
					courseInDb.Name = edit.Course.Name;
					courseInDb.Description = edit.Course.Description;
					_context.SaveChanges();
					return RedirectToAction("Index");
				}
			}

			var courseVM = new CourseCategoriesViewModel()
			{
				Categories = _context.Categories.ToList(),
				Course = edit.Course,
			};
			return View(courseVM);
		}

		//GET: Delete
		[HttpGet]

		public ActionResult Delete(int id)
		{
			var courseInDb = _context.Courses.SingleOrDefault(c => c.Id == id);

			if (courseInDb == null)
			{
				return HttpNotFound();
			}
			_context.Courses.Remove(courseInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteConfirmed(int id)
		//{
		//  Course course = _context.Courses.Find(id);
		//  _context.Courses.Remove(course);
		//  _context.SaveChanges();
		//  return RedirectToAction("Index");
		//}
	}
}