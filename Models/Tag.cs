using System.Collections.Generic;

namespace dojoQA.Models
{
    public class Tag : BaseEntity
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public List<QuestionTag> Questions { get; set; }

        public Tag() {
            Questions = new List<QuestionTag>();
        }
    }
}