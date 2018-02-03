using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace dojoQA.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Dojo Dojo { get; set; }

        public string LinkedInAccountURL { get; set; }

        public List<Question> QuestionsAsked { get; set; }

        public List<Answer> AnswersProvided { get; set; }

        public List<QuestionVote> QuestionsVotedOn { get; set; }

        public List<AnswerVote> AnswersVotedOn { get; set; }
    }
}