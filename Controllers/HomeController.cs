using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dojoQA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dojoQA.Controllers
{
    public class HomeController : Controller
    {
        private DojOverflowContext _context;
        private UserManager<ApplicationUser> _userManager;
        
        public HomeController(DojOverflowContext c, UserManager<ApplicationUser> um) {
            _context = c;
            _userManager = um;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            //send up Dojo list to populate dropdown selection
            return View();
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        public async Task<JsonResult> RegisterUser([FromBody] ApplicationUser user)
        {
            //really the only backend validations should be existing email (or username, when I get to that)
            //front end validations should already be taken care of
            var result = await _userManager.CreateAsync(user, user.Password);
            if (result.Succeeded && result.Errors.Count() == 0) {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Json(user);
            }
            else {
                //determine how to send back specific errors--create error object?
                    //or just set regex for frontend password
                //does not meet authorization requirements
                return Json(result.Errors);
            }

        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
