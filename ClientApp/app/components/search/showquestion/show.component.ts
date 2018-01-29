import { Component } from '@angular/core';
import { Question } from '../../../models/Question';
import { QuestionServerResponse } from '../../../models/QuestionServerResponse';

import { QuestionService } from '../../../services/question.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'show',
    templateUrl: './show.component.html',
    styleUrls: ['./show.component.css']
})
export class ShowQuestionComponent {   

    private id : string | null;     
    private question: QuestionServerResponse;

    constructor(private _route: ActivatedRoute, private _router: Router) {
        this._route.paramMap.subscribe(params => {
            this.id = params.get('id');
            
        })
    }
}
