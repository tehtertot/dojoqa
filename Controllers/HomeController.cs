using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dojoQA.Auth;
using dojoQA.Helpers;
using dojoQA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace dojoQA.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions; 
        
        public HomeController(UserManager<ApplicationUser> um, JwtFactory jwf, IOptions<JwtIssuerOptions> jwo) {
            _userManager = um;
            _jwtFactory = jwf;
            _jwtOptions = jwo.Value;
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
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser user)
        {
            //really the only backend validations should be existing email (or username, when I get to that)
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            //front end validations should already be taken care of
            ApplicationUser newUser = new ApplicationUser {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Email,
                Email = user.Email
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded) {
                return new OkObjectResult(new JsonResult(new { success = true, user = newUser}));
            }
            //determine how to send back specific errors--create error object?
                //or just set regex for frontend password
            //does not meet authorization requirements
            //loop through result.Errors and add errors to ModelState
                // eg. ModelState.AddModelError("login", "Invalid username and/or password");
            return BadRequest(ModelState);
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogInUser([FromBody] LoginUser login) {
            
            //should be captured by front-end validations, but just to be sure
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            ClaimsIdentity identity = await GetClaimsIdentity(login.Email, login.Password);
            if (identity == null) {
                ModelState.AddModelError("login", "Invalid username and/or password");
                return BadRequest(ModelState);
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, login.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            ApplicationUser userToVerify = await _userManager.FindByEmailAsync(email);

            //no user with matching email
            if (userToVerify == null) {
                return await Task.FromResult<ClaimsIdentity>(null); 
            }

            //password is correct
            if (await _userManager.CheckPasswordAsync(userToVerify, password)) {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(email, userToVerify.Id));
            }

            //any other errors (i.e. password is incorrect)
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
