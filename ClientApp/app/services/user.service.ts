import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/User';
import { UserServerResponse } from '../models/UserServerResponse';
import { Token } from '../models/Token';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class UserService {

  private loggedIn = false;
  data: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);

  constructor(private _http: HttpClient) { }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  registerUser(user: User): Observable<Token> {
    return this._http.post<Token>('/register', user);
  }

  setLoggedInStatus(isLoggedIn: boolean) : void {
    this.loggedIn = isLoggedIn;
  }

  loginUser(user: User): Observable<Token> {
    return this._http.post<Token>('/login', user);
  }

  getUserInfo(): Observable<UserServerResponse> {
    let authToken = localStorage.getItem('auth_token');
    let headers = new HttpHeaders({
      'Content-Type': 'application/json', 
      'Authorization': `Bearer ${authToken}`
      });
    
    return this._http.get<UserServerResponse>("/profile", {headers});
  }
}
