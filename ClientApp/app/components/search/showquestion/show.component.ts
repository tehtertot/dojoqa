import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

//model imports
import { QuestionWithAnswersResponse } from '../../../models/QuestionWithAnswersResponse';
import { Answer } from '../../../models/Answer';

//other services
import { QuestionService } from '../../../services/question.service';
import { UserService } from '../../../services/user.service';

@Component({
    selector: 'show',
    templateUrl: './show.component.html',
    styleUrls: ['./show.component.css']
})
export class ShowQuestionComponent implements OnInit {   
    private userId: string | null;
    private question: QuestionWithAnswersResponse;
    private newAnswer : Answer = new Answer();

    constructor(private _router: Router, private _questionService: QuestionService, private _route: ActivatedRoute, private _zone: NgZone, private _userService: UserService) { }

    ngOnInit() {
        this.question = this._route.snapshot.data.question;
        this.userId = this._userService.getUserId();
    }

    submitAnswer() {
        this._questionService.addAnswer(this.newAnswer, this.question.questionId)
            .subscribe((updatedQuestion) => {
                this.question = updatedQuestion;
            });
    }

    voteForQuestion(id: number) {
        //can only vote if not yet voted
        if (this.question.canVote) {
            this._questionService.voteForQuestion(id)
                .subscribe((res) => {
                    if (res) {
                        this._zone.run(() => {
                            //update question for view purposes; already updated in DB
                            this.question.canVote = false;
                            this.question.votes++;
                        });
                    }
                });
        }
    }

    voteForAnswer(id: number) {
        //can only vote if not yet voted
        this._questionService.voteForAnswer(id)
            .subscribe((res) => {
                this._zone.run(() => {
                    let thisAnswer = this.question.answers.filter(a => a.answerId == id)[0];
                    thisAnswer.canVote = false;
                    if (res) {
                        thisAnswer.votes++;
                    }
                });
        });
    }
}
