using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data.Entity;
using Academy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using PagedList;
using PagedList.Mvc;
using Academy.Infrastructure;

namespace Academy.Controllers
{
    public class DisciplinesController : Controller
    {
        ApplicationDbContext context;
        public ActionResult Index(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            context = new ApplicationDbContext(); // initialize context
            var disc = from obj in context.Courses
                              select obj;
            switch (sortOrder)
            {
                case "spaces_taken":
                    disc = disc.OrderByDescending(obj=> obj.Spaces);
                    break;
                case "spaces_free":
                    disc = disc.OrderBy(obj => obj.Spaces);break;
                default:
                    disc = disc.OrderBy(obj=>obj.Name);
                    break;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(disc.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Index(string button) // button holds the course name that the user chose
        {
            ViewBag.chosen = button; // TOREMOVE
            context = new ApplicationDbContext(); // initialize context
            var userStore = new UserStore<ApplicationUser>(context);
            var roleStore = new RoleStore<ApplicationRole>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);
            var currentUser = userManager.FindById(User.Identity.GetUserId()); // get the current user
            if (currentUser != null) // if there is a user logged in 
            {
                
                ViewBag.roles = currentUser.Roles.Select(obj=>obj.Role.Name); // TOREMOVE
                  // if user is already in this course don't allow him to sign again.
                if(userManager.IsInRole(currentUser.Id,button))
                {
                    return RedirectToAction("Index", "Home");
                }
                var course = context.Courses.Where(obj=>obj.Name==button).Single();
                if(course.Spaces<course.TotalSpaces) // there is free space
                {
                    userManager.AddToRole(currentUser.Id, button);
                    course.Spaces++;
                    context.SaveChanges(); // save 
                }
                else // if its full
                {
                    ViewBag.RoleFull = true;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            // Redirect to my course tab
            return RedirectToAction("MyDisc", "Disciplines");

        }
        [Authorize]
        public ActionResult MyDisc()
        {
            context = new ApplicationDbContext(); // initialize context
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var currentUser = userManager.FindById(User.Identity.GetUserId()); // get the current user
            
            return View(currentUser.Roles);
        }
        [Authorize]
        [HttpPost] // Remove course
        public ActionResult MyDisc(string button)
        {
           
                context = new ApplicationDbContext(); // initialize context
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var currentUser = userManager.FindById(User.Identity.GetUserId()); // get the current user
                var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
                userManager.RemoveFromRole(currentUser.Id, roleManager.FindByName(button).Name); // remove the role(course) that user chose
            
                // If user leaves course , free 1 space.
                var course= context.Courses.Where(obj => obj.Name == button).SingleOrDefault();
                course.Spaces--;
                context.SaveChanges();

            return View(currentUser.Roles);

        }
        public ActionResult CreateDisc()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult GoToDisc(string button)
        {
            context = new ApplicationDbContext(); // initialize context
           var currentCourse = context.Courses.Where(obj => obj.Name == button).SingleOrDefault();
           var lessons = (from obj in context.Lessons
                          where obj.LinkedCourse.Name == button
                          select obj).ToList();
            return View(lessons);
        }
       
        [Authorize]
        [HttpPost]
        public ActionResult Lesson(string submit)
        {
   
           StringBuilder lesson = new StringBuilder();
                string line = string.Empty;
                try
                {
                    using (StreamReader sr = new StreamReader(Server.MapPath(submit)))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            line = line.Replace("[[<]]", "&lt;"); // replace the characters with their correspodings
                            line = line.Replace("[[>]]", "&gt;");
                            line = line.Replace("[[&]]", "&amp;");
                            lesson.AppendLine(line);

                        }
                    }
                }
            catch(System.IO.FileNotFoundException)
                {
                  ViewBag.NotFound = true;
                return View();
                }
 
           return View((object)lesson.ToString());
        }


      
}
}