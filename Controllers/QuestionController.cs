
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

        [HttpPost("new")]
        public QuestionViewModel addQuestion([FromBody] InputQuestion question) {
                string userId = _caller.Claims.Single(c => c.Type == "id").Value;
                ApplicationUser user = _context.Users.SingleOrDefault(u => u.Id == userId);
                Question q = new Question();
                q.AskedBy = user;
                q.QuestionText = question.QuestionText;
                //save tags
                foreach (TagViewModel item in question.Tags) {
                    // _context.QuestionTags.Add(new QuestionTag());
                    q.Tags.Add(new QuestionTag(item.tagId));
                }

                try {
                    _context.Questions.Add(q);
                    _context.SaveChanges();
                    return new QuestionViewModel(q);
                }
                catch {
                    return null;
                }
        }

        [HttpGet("tags")]
        public List<TagViewModel> getAllTags() {
            return _context.Tags.ToList().Join(
                _context.Categories,
                t => t.Category,
                c => c,
                (t, c) => {
                    TagViewModel tvm = new TagViewModel();
                    tvm.tagId = t.TagId;
                    tvm.name = t.Name;
                    tvm.categoryId = c.CategoryId;
                    tvm.categoryName = c.Name;
                    return tvm;
                })
            .OrderBy(tvm => tvm.name).ToList();
        }
    }
}