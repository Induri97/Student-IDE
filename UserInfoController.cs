using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Student_Courses.Data;
using Student_Courses.Models;

namespace Student_Courses.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly Student_CoursesContext _context;
        public const string Admin = "12admin@k.com";

            public UserInfoController(Student_CoursesContext context)
            {
                _context = context;
            }

            // GET: /Account/Register
            public ActionResult RegisterUser()
            {
                return View();
            }

            // POST: /Account/Register
            [HttpPost]
            public ActionResult RegisterUser(UserInfo user)
            {
                if (ModelState.IsValid)
                {
                     _context.UserInfo.Add(user);
                     _context.SaveChanges();
                     return View("Success");
                }
                return View(user);
            }

            // GET: /Account/Login
            public ActionResult Login()
            {
            return View();
            }

            // POST: /Account/Login
            [HttpPost]
            public ActionResult Login(string email, string password)
            {
                var user = _context.UserInfo.FirstOrDefault(s => s.Email == email && s.Password == password);

                if (user != null)
                {
                    int id = user.ID;
                    if (user.Password == password && user.Email == email)
                    {
                        // Authentication successful, redirect to dashboard or desired page
                        return RedirectToAction("Index", "CourseInfo", new { id });

                    }
                }
                else
                {
                    // Password/Email does not match, display error message
                    ViewBag.AccountError = "Account not Found. Please Enter Valid Credentials";
                    return View();
                }
                return View();
            }

            public ActionResult About()
            {
                return View();
            }


            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }

