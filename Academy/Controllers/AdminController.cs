using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using Academy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Academy.Infrastructure;

namespace Academy.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AdminController : Controller
    {
        ApplicationDbContext context;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDisc()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddDisc(Course cs)
        {
            context = new ApplicationDbContext();
            // Check for unique name
            if (context.Courses.Where(obj => obj.Name == cs.Name).Count()>0)
            {
                ModelState.AddModelError("WrongName", "This name is already taken!");
            }
            if (ModelState.IsValid) // if everything is ok add the course
            {

                var RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
                RoleManager.Create(new ApplicationRole { Name = cs.Name }); // create the corresponding role
                context.Courses.Add(cs);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult RemoveDisc()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RemoveDisc(string deleteName)
        {
            List<Lesson> deletedLessons; // this will hold all the lessons that must be deleted
            context = new ApplicationDbContext();
            if (deleteName == null)
            {
                ModelState.AddModelError("NameEmpty", "Please enter a name!"); // if field is empty
                return View();
            }
            else
            {
                deletedLessons = new List<Lesson>(); // set the list
                var deletedCourse = context.Courses.Where(crs => crs.Name == deleteName);// find the course and delete it

               
                foreach (var item in deletedCourse)
                {
                    foreach (var lesson in context.Lessons)
                    {
                        if(item.CourseId==lesson.LinkedCourse.CourseId) // if there's a match
                        {
                            if ((System.IO.File.Exists(Server.MapPath(lesson.PathToLesson)))) // delete the file if exist
                            {
                                System.IO.File.Delete(Server.MapPath(lesson.PathToLesson));
                            }
                            item.Lessons = null; // lessons point to NULL
                            lesson.LinkedCourse = null; // courses point to NULL 
                            context.Lessons.Remove(lesson); // remove the lessons
                        }
                    }
                }
                context.Courses.RemoveRange(deletedCourse); // delete the courses
                context.Roles.Remove(context.Roles.Where(role => role.Name == deleteName).FirstOrDefault()); // now delete the role
                context.SaveChanges(); // save the changes
                return RedirectToAction("Index"); 

            }

        }
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(string submit)
        {
            context = new ApplicationDbContext(); // initialize context

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.AddToRole(submit, "Administrator");
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult RemoveUser(string submit)
        {
            context = new ApplicationDbContext(); // initialize context

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.RemoveFromRole(submit, "Administrator");
            return RedirectToAction("Index");

        }

        public ActionResult AddLesson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLesson(Lesson lesson, string dropDown)
        {
            
            context = new ApplicationDbContext();
            string pathName = string.Empty;
            if (context.Lessons.Where(obj => obj.Name == lesson.Name).Count()>0) // check for unique name
            {
                ModelState.AddModelError("NameWrong", "There is already a lesson with that name!");
            }
            if (ModelState.IsValid)
            {
                
                var linkedCrs = context.Courses.Where(obj => obj.Name == dropDown).SingleOrDefault(); // select the course
                lesson.LinkedCourse = linkedCrs; // match the lesson with the course

                int numOfLessonInCourse = context.Lessons.Where(obj => obj.LinkedCourse.Name == dropDown).Count();// how many lessons are there
                pathName = "../Views/Disciplines/Lessons/" + linkedCrs.Name + (numOfLessonInCourse + 1).ToString(); // set the name (example CSS1,CSS2,etc.)
                lesson.PathToLesson = pathName + ".txt"; // set the path

                using (var fileStream = new FileStream(Server.MapPath(pathName + ".txt"), FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(fileStream)) // this is the file name
                {
                    sw.Write(lesson.Content); // write the lesson in the txt file
                };
                context.Lessons.Add(lesson);
                context.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }
        public ActionResult RemoveLesson()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RemoveLesson(string submit)
        {
            context = new ApplicationDbContext();
            var lesson = context.Lessons.Where(obj => obj.Name == submit).SingleOrDefault();
            if ((System.IO.File.Exists(Server.MapPath(lesson.PathToLesson)))) // delete the file if exist
            {
                System.IO.File.Delete(Server.MapPath(lesson.PathToLesson));
            }
            context.Lessons.Remove(lesson);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}