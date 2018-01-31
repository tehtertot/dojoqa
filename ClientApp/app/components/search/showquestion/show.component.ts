import { Component, OnInit } from '@angular/core';
import { Question } from '../../../models/Question';
import { QuestionWithAnswersResponse } from '../../../models/QuestionWithAnswersResponse';
import { Answer } from '../../../models/Answer';

import { QuestionService } from '../../../services/question.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'show',
    templateUrl: './show.component.html',
    styleUrls: ['./show.component.css']
})
export class ShowQuestionComponent implements OnInit {   

    private id : string | null;     
    private question: QuestionWithAnswersResponse;
    private newAnswer : Answer = new Answer();

    constructor(private _route: ActivatedRoute, private _router: Router, private _questionService: QuestionService) {
        this._route.paramMap.subscribe(params => {
            this.id = params.get('id');
        });
    }

    ngOnInit() {
        this._questionService.getQuestion(this.id)
            .subscribe((question) => {
                console.log("received question");
                console.log(question);
                this.question = question;
            })
    }

    submitAnswer() {
        this._questionService.addAnswer(this.newAnswer, this.id)
            .subscribe((updatedQuestion) => {
                this.question = updatedQuestion;
                this._router.navigate(['/search/questions']);
            })
    }
}
