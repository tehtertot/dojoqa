using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dojoQA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dojoQA.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("/leaderboard")]
    public class LeaderboardController : Controller
    {
        private DojOverflowContext _context;
        
        public LeaderboardController(DojOverflowContext context) {
            _context = context;
        }

        [HttpGet("{type}")]
        public List<Leader> GetLeaders(string type) {
            List<ApplicationUser> allUsers = _context.Users.ToList();
            List<Leader> top = new List<Leader>();
            foreach (ApplicationUser u in allUsers) {
                Leader l = new Leader(u);
                if (type == "questions") {
                    IEnumerable<Question> questionsByUser = _context.Questions.Where(q => q.AskedBy == u);
                    l.count = questionsByUser.Sum(q => q.Upvotes) + questionsByUser.Count();
                }
                else if (type == "answers") {
                    IEnumerable<Answer> answersByUser = _context.Answers.Where(a => a.AnsweredBy == u);
                    l.count = answersByUser.Sum(a => a.Votes) + answersByUser.Count();
                }
                top.Add(l);
            }
            return top.OrderByDescending(l => l.count).Where(l => l.count > 0).Take(10).ToList();
        }
        
    }
}
