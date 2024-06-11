using Assessment.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AssessmentWeb.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Public home page view
        }
    }
}
