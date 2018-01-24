
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dojoQA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dojoQA.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("/questions")]
    public class QuestionController : Controller
    {
        private DojOverflowContext _context;
        private ClaimsPrincipal _caller;
        
        public QuestionController(DojOverflowContext context, IHttpContextAccessor contextAccess) {
            _caller = contextAccess.HttpContext.User;
            _context = context;
        }

        [HttpGet("")]
        public List<QuestionViewModel> getAllQuestions() {
            return _context.Questions.ToList().Join(
                _context.ApplicationUsers,
                q => q.AskedBy,
                u => u,
                (q, u) => { 
                    QuestionViewModel qvm = new QuestionViewModel();
                    qvm.QuestionId = q.QuestionId;
                    qvm.QuestionText = q.QuestionText;
                    qvm.AskedById = u.Id;
                    qvm.AskedByFirstName = u.FirstName;
                    qvm.AskedByLastName = u.LastName;
                    qvm.CreatedAt = q.CreatedAt;
                    return qvm;
                }
            ).ToList();
        }

        [HttpPost("/new")]
        public bool addQuestion([FromBody] Question question) {
            try {
                string userId = _caller.Claims.Single(c => c.Type == "id").Value;
                ApplicationUser user = _context.Users.SingleOrDefault(u => u.Id == userId);
                question.AskedBy = user;
                _context.Questions.Add(question);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}