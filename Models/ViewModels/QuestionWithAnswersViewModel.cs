using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoQA.Models
{
    public class QuestionWithAnswersViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerView> Answers { get; set; }
        public string AskedById { get; set; }
        public string AskedByFirstName { get; set; }
        public string AskedByLastName { get; set; }
        public DateTime CreatedAt { get; set; }

        public QuestionWithAnswersViewModel() { }

        public QuestionWithAnswersViewModel(Question question) {
            QuestionId = question.QuestionId;
            QuestionText = question.QuestionText;
            AskedById = question.AskedBy.Id;
            AskedByFirstName = question.AskedBy.FirstName;
            AskedByLastName = question.AskedBy.LastName;
            Answers = new List<AnswerView>();
            addAnswers(question.Answers);            
        }

        private void addAnswers(List<Answer> allAnswers) {
            foreach (Answer a in allAnswers) {
                AnswerView av = new AnswerView();
                av.AnsweredById = a.AnsweredBy.Id;
                av.AnsweredByName = a.AnsweredBy.FirstName + " " + a.AnsweredBy.LastName;
                av.AnswerId = a.AnswerId;
                av.AnswerText = a.AnswerText;
                Answers.Add(av);
            }
        }
    }

    
}