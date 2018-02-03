// tools
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { UserAuthInterceptor } from '../services/userauth.interceptor';
import 'rxjs';

// models
import { Question } from '../models/Question';
import { Answer } from '../models/Answer';
import { CategoryTag } from '../models/CategoryTag';
import { QuestionWithAnswersResponse } from '../models/QuestionWithAnswersResponse';
import { QuestionServerResponse } from '../models/QuestionServerResponse';
// import { Tag } from '../models/Tag';



@Injectable()
export class QuestionService {

  constructor(private _http: HttpClient) { }
  
  addQuestion(question: Question): Observable<QuestionServerResponse> {
    return this._http.post<QuestionServerResponse>('/questions/new', question);
  }

  getAllQuestions(): Observable<Array<QuestionWithAnswersResponse>> {
    return this._http.get<Array<QuestionWithAnswersResponse>>("/questions");
  }

  // getAllTags(): Observable<Array<Tag>> {
  //   return this._http.get<Array<Tag>>("/questions/tags");
  // }

  getAllTags(): Observable<Array<CategoryTag>> {
    return this._http.get<Array<CategoryTag>>("/questions/tags");
  }

  getQuestion(id: string | null): Observable<QuestionWithAnswersResponse> {
    return this._http.get<QuestionWithAnswersResponse>(`/questions/${id}`);
  }

  addAnswer(answer: Answer, id: string | null) : Observable<QuestionWithAnswersResponse> {
    return this._http.post<QuestionWithAnswersResponse>(`/questions/answer/${id}`, answer);
  }

  voteForQuestion(questionId: number) {
    return this._http.get(`/questions/vote/${questionId}`);
  }

  voteForAnswer(answerId: number) {
    return this._http.get(`/questions/answer/vote/${answerId}`);
  }
}
