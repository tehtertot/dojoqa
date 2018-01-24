using System.Linq;
using System.Security.Claims;
using dojoQA.Auth;
using dojoQA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dojoQA.Controllers
{
    [Authorize(Policy = "ApiUser")]
    public class ProfileController : Controller
    {
        private DojOverflowContext _context;
        private ClaimsPrincipal _caller;
        
        public ProfileController(DojOverflowContext context, IHttpContextAccessor contextAccess) {
            _caller = contextAccess.HttpContext.User;
            _context = context;
        }

        [HttpGet("/profile")]
        public ApplicationUser showUserInfo() {
            string userId = _caller.Claims.Single(c => c.Type == "id").Value;
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }
    }
}