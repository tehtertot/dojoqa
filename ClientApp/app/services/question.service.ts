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

  //*********** QUESTIONS *************//
  getAllQuestions(): Observable<Array<QuestionWithAnswersResponse>> {
    return this._http.get<Array<QuestionWithAnswersResponse>>("/questions");
  }

  addQuestion(question: Question) : Observable<boolean> {
    return this._http.post<boolean>('/questions/new', question);
  }

  getQuestion(id: string | null): Observable<QuestionWithAnswersResponse> {
    return this._http.get<QuestionWithAnswersResponse>(`/questions/${id}`);
  }

  voteForQuestion(questionId: number) : Observable<boolean> {
    return this._http.get<boolean>(`/questions/vote/${questionId}`);
  }

  //************ ANSWERS **************//
  addAnswer(answer: Answer, id: number) : Observable<QuestionWithAnswersResponse> {
    return this._http.post<QuestionWithAnswersResponse>(`/questions/answer/${id}`, answer);
  }

  voteForAnswer(answerId: number) : Observable<boolean> {
    return this._http.get<boolean>(`/questions/answer/vote/${answerId}`);
  }

  //************** TAGS ***************//
  getAllTags(): Observable<Array<CategoryTag>> {
    return this._http.get<Array<CategoryTag>>("/questions/tags");
  }

  // getTagsByCategory(category: string): Observable<CategoryTag> {
  //   return this._http.get<CategoryTag>(`/questions/tags/${category}`);
  // }

}
