using System.ComponentModel.DataAnnotations;

namespace dojoQA.Models
{
    public class Answer : BaseEntity
    {
        [Key]
        public int AnswerId { get; set; }

        [MinLength(10, ErrorMessage="Please provide a more descriptive answer.")]
        public string AnswerText { get; set; }

        public ApplicationUser AnsweredBy { get; set; }

        public Question Question { get; set; }

        //additional fields: upvotes, downvotes

        public Answer() { }
    }
}