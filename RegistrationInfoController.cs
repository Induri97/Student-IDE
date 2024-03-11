using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Student_Courses.Data;
using Student_Courses.Models;

namespace Student_Courses.Controllers
{
    public class RegistrationInfoController : Controller
    {
        private readonly Student_CoursesContext _context;

        public RegistrationInfoController(Student_CoursesContext context)
        {
            _context = context;
        }
        // GET: /Registration/Register
        public ActionResult ViewCourses()
        {
            return View();
        }

        // POST: /Registration/Register
        [HttpPost]
        public ActionResult ViewCourses(CourseInfo course, int StdId)
        {
            var Reg = new RegistrationInfo();
            Reg.CourseID = course.CourseID;
            Reg.StudentID = StdId;
            Reg.DateRegistered = DateTime.Now;
            ViewBag.ID = StdId;
            var coursename = _context.CourseInfo.FirstOrDefault(u => u.CourseID == course.CourseID); ;
            var std_name = _context.UserInfo.FirstOrDefault(u => u.ID == StdId);
            
            ViewBag.StudentName = std_name.Name;
            ViewBag.courseName = coursename.CourseName;

            //To check if a student has his maximum registration of courses
            int totalRegisteredCourses = _context.RegistrationInfo.Count(item => item.StudentID == StdId);
            if (totalRegisteredCourses >= 3)
            {
                ViewBag.Error = "StudentCoursesLimit";
                return View();
            }

            // To check if the course has reached the maximum capacity of 5 students
            int totalStudentsRegisteredForCourse = _context.RegistrationInfo.Count(item => item.CourseID == course.CourseID);
            if (totalStudentsRegisteredForCourse >= 5)
            {
                ViewBag.Error = "CourseCapacityReached";
                return View();
            }

            //To not allow a student to register for the same course more than once
            bool isAlreadyRegistered = _context.RegistrationInfo.Any(r => r.StudentID == StdId && r.CourseID == course.CourseID);
            if (isAlreadyRegistered)
            {
                ViewBag.Error = "AlreadyRegistered";
                return View(); 
            }


            //To allow a student to register for a course if all above conditions are met
            if (Reg.CourseID!=0 && Reg.StudentID!=0)
            {
                _context.RegistrationInfo.Add(Reg);
                _context.SaveChanges();
                var ViewCourses = _context.RegistrationInfo.Where(item=> item.StudentID ==StdId).ToList();
                 
                return View(ViewCourses);
            }

            return View();
        }
        
        public ActionResult Details(int? id)
        {
            var registration = _context.RegistrationInfo.Where(i=>i.CourseID==id).ToList();
            
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        public ActionResult RegisteredCourses(int ID)
        {
            var std_name = _context.UserInfo.FirstOrDefault(u => u.ID == ID);
            ViewBag.StudentName = std_name.Name;
            var registration = _context.RegistrationInfo.Where(i => i.StudentID == ID).ToList();
            foreach (var reg in registration)
            {
                reg.CoursesRegistered = _context.CourseInfo
                                                    .Where(c => c.CourseID == reg.CourseID)
                                                    .ToList();
            }
            return View(registration);
        }
    }
}

