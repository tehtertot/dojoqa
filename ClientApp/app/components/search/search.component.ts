import { Component, OnInit } from '@angular/core';
import { Question } from '../../models/Question';
import { QuestionServerResponse } from '../../models/QuestionServerResponse';

import { QuestionService } from '../../services/question.service';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent {
    public question: Question = new Question();
    public allQuestions: Array<QuestionServerResponse>;

    constructor(private _questionService: QuestionService) { }
    
    ngOnInit() {
        this._questionService.getAllQuestions()
            .subscribe((questions) => {
                this.allQuestions = questions;
            })
    }
            
    public addQuestion() {
        if (this.question.QuestionText.trim() == "") {
            this.question.QuestionText = "";
            window.alert("Question cannot be empty");
        }
        else {
            this.question.QuestionText = this.question.QuestionText.trim();
            this._questionService.addQuestion(this.question)
                .subscribe(() => this.question = new Question());
        }
    }
        
}
