using System.ComponentModel.DataAnnotations.Schema;

namespace dojoQA.Models
{
    public class QuestionTag
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public QuestionTag() { }

        public QuestionTag(int tag) {
            TagId = tag;
        }
    }
}