import { Component, OnInit } from '@angular/core';

import { Question } from '../../../models/Question';
import { QuestionServerResponse } from '../../../models/QuestionServerResponse';

import { QuestionService } from '../../../services/question.service';
import { Router } from '@angular/router';

@Component({
    selector: 'questions',
    templateUrl: './questions.component.html',
    styleUrls: ['./questions.component.css']
})
export class QuestionsComponent {
    private allQuestions: Array<QuestionServerResponse>;
    private searchStr: string = "";

    constructor(private _questionService: QuestionService, private _router: Router) { }
    
    ngOnInit() {
        this._questionService.getAllQuestions()
            .subscribe((questions) => {
                this.allQuestions = questions;
            })
    }
            
    public answerQuestion(id: number) {
        this._router.navigate(['/search/questions', id]);
    }

    public askQuestion() {
        this._router.navigate(['/search/ask']);
    }
        
}
