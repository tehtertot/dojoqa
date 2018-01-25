import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Question } from '../models/Question';
import 'rxjs';
import { Observable } from 'rxjs/Observable';
import { QuestionServerResponse } from '../models/QuestionServerResponse';
import { UserAuthInterceptor } from '../services/userauth.interceptor';
import { Tag } from '../models/Tag';

@Injectable()
export class QuestionService {

  constructor(private _http: HttpClient) { }
  
  addQuestion(question: Question): Observable<QuestionServerResponse> {
    return this._http.post<QuestionServerResponse>('/questions/new', question);
  }

  getAllQuestions(): Observable<Array<QuestionServerResponse>> {
    return this._http.get<Array<QuestionServerResponse>>("/questions");
  }

  getAllTags(): Observable<Array<Tag>> {
    return this._http.get<Array<Tag>>("/questions/tags");
  }

}
