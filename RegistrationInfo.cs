using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Student_Courses.Models
{
    public class RegistrationInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime DateRegistered { get; set; }

        [ForeignKey("CourseID")]
        public ICollection<CourseInfo> CoursesRegistered { get; set; }
    }
}
