using Assessment.Models;
using Assessment.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssessmentWeb.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = SD.Role_Employee)]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ViewBag.UserName = user.UserName;

            return View();
        }
    }
}
