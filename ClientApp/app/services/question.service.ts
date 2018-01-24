import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
// import { Response, Headers } from '@angular/http';
import { Question } from '../models/Question';
import 'rxjs';
import { Observable } from 'rxjs/Observable';
import { QuestionServerResponse } from '../models/QuestionServerResponse';

@Injectable()
export class QuestionService {

  constructor(private _http: HttpClient) { }
  
  addQuestion(question: Question): Observable<boolean> {
    let authToken = localStorage.getItem('auth_token');
    let headers = new HttpHeaders({
      'Content-Type': 'application/json', 
      'Authorization': `Bearer ${authToken}`
      });
      
    return this._http.post<boolean>('/questions/new', question, {headers});
  }

  getAllQuestions(): Observable<Array<QuestionServerResponse>> {
    let authToken = localStorage.getItem('auth_token');
    let headers = new HttpHeaders({
      'Content-Type': 'application/json', 
      'Authorization': `Bearer ${authToken}`
      });
    
    return this._http.get<Array<QuestionServerResponse>>("/questions", {headers});
  }

}
