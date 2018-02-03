namespace dojoQA.Models
{
    public class InputQuestion
    {
        public string QuestionTitle { get; set; }
        public string QuestionText { get; set; }
        public TagViewModel[] Tags { get; set; }
    }
}