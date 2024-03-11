using Microsoft.AspNetCore.Mvc;

namespace Student_Courses.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Logout()
        {
            return View();
        }
    }
}
