using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dojoQA.Models
{
    public class Question : BaseEntity
    {
        [Key]
        public int QuestionId { get; set; }

        [MinLength(10, ErrorMessage="Please provide a more descriptive question.")]
        public string QuestionText { get; set; }

        public ApplicationUser AskedBy { get; set; }

        public List<Answer> Answers { get; set; }

        //additional fields: tags, upvotes, downvotes

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}