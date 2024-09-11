using Microsoft.AspNetCore.Mvc;
using PROG6212___CMCS___ST10082700.Models;

namespace PROG6212___CMCS___ST10082700.Controllers
{
    public class LecturerController : Controller
    {
        public IActionResult Dashboard()
        {
            var model = new LecturerDashboardModel
            {
                LecturerUsername = "lecturer@keemouniversity.com"
            };

            return View(model);
        }
    }
}