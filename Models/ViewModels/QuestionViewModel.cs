using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoQA.Models
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AskedById { get; set; }
        public string AskedByFirstName { get; set; }
        public string AskedByLastName { get; set; }
        public DateTime CreatedAt { get; set; }

        public QuestionViewModel() { }

        public QuestionViewModel(Question question) {
            QuestionId = question.QuestionId;
            QuestionText = question.QuestionText;
            AskedById = question.AskedBy.Id;
            AskedByFirstName = question.AskedBy.FirstName;
            AskedByLastName = question.AskedBy.LastName;
        }
    }
}