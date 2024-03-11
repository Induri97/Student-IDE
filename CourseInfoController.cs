using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Courses.Data;
using Student_Courses.Models;

namespace Student_Courses.Controllers
{
    public class CourseInfoController : Controller
    {
        private readonly Student_CoursesContext _context;

        public CourseInfoController(Student_CoursesContext context)
        {
            _context = context;
        }

        // GET: CourseInfo
        public ActionResult Index(int id)
        {
            var user = _context.UserInfo.FirstOrDefault(u => u.ID == id); 

            if (user != null && user.IsAdmin)
            {
                ViewBag.IsAdmin = true;
                var coursesWithRegistrations = _context.CourseInfo.ToList();
                var registrationCounts = new Dictionary<int, int>();

                // Retrieve registration counts for each course
                foreach (var course in coursesWithRegistrations)
                {
                    var count = _context.RegistrationInfo.Count(x => x.CourseID == course.CourseID);
                    registrationCounts.Add(course.CourseID, count);
                }

                ViewBag.Registrations = registrationCounts;
                return View(coursesWithRegistrations);
            }
            else 
            {
                ViewBag.IsAdmin = true;
                ViewBag.SID = id;
                return View(_context.CourseInfo.ToList());
            }
            
        }


        public ActionResult Register(int sid)
        {
            //int m = sid;
            ViewBag.StdId = sid;
            var courses = _context.CourseInfo.ToList();

            return View(courses);
        }
    }
}
