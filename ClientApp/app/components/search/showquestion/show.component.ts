import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

//model imports
import { QuestionWithAnswersResponse } from '../../../models/QuestionWithAnswersResponse';
import { Answer } from '../../../models/Answer';

//other services
import { QuestionService } from '../../../services/question.service';

@Component({
    selector: 'show',
    templateUrl: './show.component.html',
    styleUrls: ['./show.component.css']
})
export class ShowQuestionComponent implements OnInit {   

    private id : string | null;     
    private question: QuestionWithAnswersResponse;
    private newAnswer : Answer = new Answer();

    constructor(private _router: Router, private _questionService: QuestionService, private _route: ActivatedRoute) {
        // this._route.paramMap.subscribe(params => {
        //     this.id = params.get('id');
        // });
    }

    ngOnInit() {
        this.question = this._route.snapshot.data.question;
    }

    submitAnswer() {
        this._questionService.addAnswer(this.newAnswer, this.id)
            .subscribe((updatedQuestion) => {
                this._router.navigate(['/search/questions']);
            });
    }

    voteForQuestion(id: number) {
        //can only vote if not yet voted
        if (this.question.canVote) {
            this._questionService.voteForQuestion(id)
                .subscribe(() => {
                    //update question for view purposes; already updated in DB
                    this.question.canVote = false;
                    this.question.votes++;
                });
        }
    }

    voteForAnswer(id: number) {
        //can only vote if not yet voted
        this._questionService.voteForAnswer(id)
            .subscribe(() => {
                // this.question.answers.
        });
    }
}
