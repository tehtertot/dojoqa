
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
        public List<QuestionWithAnswersViewModel> getAllQuestions() {
            List<Question> allQuestions = _context.Questions.Include(q => q.AskedBy).Include(q => q.Answers).ThenInclude(a => a.AnsweredBy).Include(q => q.Tags).ThenInclude(t => t.Tag).ThenInclude(x => x.Category).OrderByDescending(q => q.CreatedAt).ToList();
            List<QuestionWithAnswersViewModel> allQuestionsForView = new List<QuestionWithAnswersViewModel>();
            foreach (Question q in allQuestions) {
                allQuestionsForView.Add(new QuestionWithAnswersViewModel(q));
            }
            return allQuestionsForView;
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

        // [HttpGet("tags")]
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

        [HttpGet("tags")]
        public List<CategoryWithTagsViewModel> getAllTagsByCategories() {
            List<Category> allCategories = _context.Categories.Include(c => c.AssociatedTags).ToList();
            List<CategoryWithTagsViewModel> categoriesWithTags = new List<CategoryWithTagsViewModel>();
            foreach (Category c in allCategories)
            {
                CategoryWithTagsViewModel cwtvm = new CategoryWithTagsViewModel(c);
                categoriesWithTags.Add(cwtvm);
            }
            return categoriesWithTags;
        }

        [HttpGet("{id:int}")]
        public QuestionWithAnswersViewModel getQuestion(int id) {
            string userId = _caller.Claims.Single(c => c.Type == "id").Value;
            
            //get full question from db (with all joins)
            Question question = _context.Questions.Include(q => q.Tags).ThenInclude(qt => qt.Tag).ThenInclude(t => t.Category).Include(q => q.AskedBy).Include(q => q.Answers).ThenInclude(a => a.AnsweredBy).SingleOrDefault(q => q.QuestionId == id);
            
            //transform into view model
            QuestionWithAnswersViewModel returnedQ =  new QuestionWithAnswersViewModel(question);
            
            //check if user has voted
            returnedQ.CanVote = _context.QuestionVotes.SingleOrDefault(q => q.QuestionId == id && q.UserId == userId) == null;
            
            return returnedQ;
        }

        [HttpPost("answer/{id:int}")]
        public QuestionWithAnswersViewModel addAnswer([FromBody] AnswerView answerView, int id) {
            string userId = _caller.Claims.Single(c => c.Type == "id").Value;
            ApplicationUser user = _context.Users.SingleOrDefault(u => u.Id == userId);
                
            Answer a = new Answer();
            a.AnsweredBy = user;
            a.AnswerText = answerView.AnswerText;
            a.Question = _context.Questions.Single(q => q.QuestionId == id);
            _context.Answers.Add(a);
            _context.SaveChanges();
            return new QuestionWithAnswersViewModel(_context.Questions.Single(q => q.QuestionId == id));
        }

        [HttpGet("vote/{id:int}")]
        public void upvoteQuestion(int id) {
            string userId = _caller.Claims.Single(c => c.Type == "id").Value;
            Question question = _context.Questions.SingleOrDefault(q => q.QuestionId == id);

            //if user has not already voted for
            if (_context.QuestionVotes.SingleOrDefault(q => q.QuestionId == id && q.UserId == userId) == null) {
                QuestionVote qv = new QuestionVote();
                qv.QuestionId = id;
                qv.UserId = userId;
                question.Upvotes++;
                _context.QuestionVotes.Add(qv);
                _context.SaveChanges();
            }
        }

        [HttpGet("answer/vote/{id:int}")]
        public void upvoteAnswer(int id) {
            string userId = _caller.Claims.Single(c => c.Type == "id").Value;
            Answer answer = _context.Answers.SingleOrDefault(a => a.AnswerId == id);

            if (_context.AnswerVotes.SingleOrDefault(a => a.AnswerId == id && a.UserId == userId) == null) {
                AnswerVote av = new AnswerVote();
                av.AnswerId = id;
                av.UserId = userId;
                answer.Votes++;
                _context.AnswerVotes.Add(av);
                _context.SaveChanges();
            }
        }
    }
}