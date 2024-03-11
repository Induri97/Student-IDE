using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Courses.Models;

namespace Student_Courses.Data
{
    public class Student_CoursesContext : DbContext
    {
        public Student_CoursesContext (DbContextOptions<Student_CoursesContext> options)
            : base(options)
        {
        }

        public DbSet<Student_Courses.Models.UserInfo> UserInfo { get; set; } = default!;
        public DbSet<Student_Courses.Models.CourseInfo> CourseInfo { get; set; } = default!;
        public DbSet<Student_Courses.Models.RegistrationInfo> RegistrationInfo { get; set; } = default!;
    }
}
