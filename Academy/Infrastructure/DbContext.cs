using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Academy.Models;

namespace Academy.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new CustomInitializer());

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        // public DbSet<User> Users { get; set; }
    }
    public class CustomInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            IList<Course> defaultCourses = new List<Course>();
            IList<Lesson> defaultLessons = new List<Lesson>();


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser rootusr = new ApplicationUser(); // Create the root user 
            rootusr.UserName = "root";
           
            rootusr.Age = 999;
            rootusr.City = "999";
            rootusr.EmailAdress = "admin@academybg.com";
            userManager.Create(rootusr, "root123");

            defaultCourses.Add(new Course() { Name = "C sharp", Spaces = 20, TotalSpaces = 20, Description = "Here you will learn the basic of C# language and reach a medium level." });
            defaultCourses.Add(new Course() { Name = "JavaScript", Spaces = 18, TotalSpaces = 20, Description = "In this course you will achieve a moderate level understanding of JavaScript language." });
            defaultCourses.Add(new Course() { Name = "HTML", Spaces = 18, TotalSpaces = 20, Description = "HTML is the standard markup language for creating Web pages." });
            defaultCourses.Add(new Course() { Name = "CSS", Spaces = 10, TotalSpaces = 20, Description = "CSS is a language that describes the style of an HTML document.CSS describes how HTML elements should be displayed.This tutorial will teach you CSS from basic to advanced." });
            context.Courses.AddRange(defaultCourses);
            // CSS lessons
            defaultLessons.Add(new Lesson() { Name = "1.Introduction to CSS", PathToLesson = "/Views/Disciplines/Lessons/CSS1.txt", Description = "These are very basic things.", LinkedCourse = defaultCourses[3] }); // get the CSS course and attach it 
            defaultLessons.Add(new Lesson() { Name = "2.CSS lesson 2", PathToLesson = "~/Views/Disciplines/Lessons/CSS2.txt", Description = "Here you are still on the very basic.", LinkedCourse = defaultCourses[3] });
            defaultLessons.Add(new Lesson() { Name = "2.CSS lesson 3", PathToLesson = "~/Views/Disciplines/Lessons/CSS3.txt", Description = "Learning about the colors", LinkedCourse = defaultCourses[3] });

            // C Sharp
            defaultLessons.Add(new Lesson() { Name = "1.Introduction to C sharp", PathToLesson = "/Views/Disciplines/Lessons/Csharp1.txt", Description = "Introduction", LinkedCourse = defaultCourses[0] }); // get the Csharp course and attach it 

            // JS
            defaultLessons.Add(new Lesson() { Name = "1.Introduction to JavaScript", PathToLesson = "/Views/Disciplines/Lessons/JavaScript1.txt", Description = "First steps in JS", LinkedCourse = defaultCourses[1] });
            defaultLessons.Add(new Lesson() { Name = "2.JavaScript lesson #2", PathToLesson = "/Views/Disciplines/Lessons/JavaScript2.txt", Description = "First steps in JS", LinkedCourse = defaultCourses[1] });
            // HTML
            defaultLessons.Add(new Lesson() { Name = "1.Introduction to HTML", PathToLesson = "/Views/Disciplines/Lessons/HTML1.txt", Description = "Introduction", LinkedCourse = defaultCourses[2] });
            defaultLessons.Add(new Lesson() { Name = "2.HTML Lesson #2", PathToLesson = "/Views/Disciplines/Lessons/HTML2.txt", Description = "Basic things.", LinkedCourse = defaultCourses[2] });


            context.Lessons.AddRange(defaultLessons);
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            roleManager.Create(new ApplicationRole() { Name = "Administrator" });
            userManager.AddToRole(rootusr.Id, "Administrator"); // Make the ROOT user admin
            foreach (var course in defaultCourses)
            {
                roleManager.Create(new ApplicationRole() { Name = course.Name });

            }

            base.Seed(context);
        }
    }
}