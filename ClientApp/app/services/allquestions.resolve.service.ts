import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { QuestionService } from './question.service';
import { QuestionWithAnswersResponse } from '../models/QuestionWithAnswersResponse';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AllQuestionsResolver implements Resolve<Array<QuestionWithAnswersResponse>> {
    constructor(private _questionService: QuestionService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : Observable<any> | Promise<any> | any {
        return this._questionService.getAllQuestions();
    }
}