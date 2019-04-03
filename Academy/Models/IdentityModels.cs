using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
namespace Academy.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "This field can not be empty.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "This field can not be empty.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "This field can not be empty.")]
        public string City { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {


    }
    public class Course
    {
        [System.ComponentModel.DataAnnotations.ScaffoldColumn(false)]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Name field can not be empty.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Spaces field can not be empty.")]
        public int Spaces { get; set; }
        [Required(ErrorMessage = "Total spaces field can not be empty.")]
        public int TotalSpaces { get; set; }
        [Required(ErrorMessage = "Description field can not be empty.")]
        public string Description { get; set; }

        public virtual IEnumerable<Lesson> Lessons { get; set; }


    }

    public class Lesson
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int LessonId { get; set; }
        [Required(ErrorMessage = "Name field can not be empty.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description field can not be empty.")]

        public string Description { get; set; }
        [AllowHtml]
        public string Content { get; set; } // let html pass so admin can add lessons. Use with caution !

        public string PathToLesson { get; set; }

        public virtual Course LinkedCourse { get; set; }
    }

}