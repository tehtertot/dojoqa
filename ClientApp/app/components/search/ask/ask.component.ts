import { Component } from '@angular/core';
import { Question } from '../../../models/Question';
import { QuestionServerResponse } from '../../../models/QuestionServerResponse';

import { QuestionService } from '../../../services/question.service';
import { Router } from '@angular/router';

@Component({
    selector: 'ask',
    templateUrl: './ask.component.html',
    styleUrls: ['./ask.component.css']
})
export class AskComponent {
    public question: Question = new Question();
    
    constructor(private _questionService: QuestionService, private _router: Router) { }
            
    public addQuestion() {
        if (this.question.QuestionText.trim() == "") {
            this.question.QuestionText = "";
            window.alert("Question cannot be empty");
        }
        else {
            this.question.QuestionText = this.question.QuestionText.trim();
            this._questionService.addQuestion(this.question)
                .subscribe((res) => {
                    this._router.navigate(['/search/questions']);
                })
        }
    }
        
}
