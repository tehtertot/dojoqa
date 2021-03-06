import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Subject } from 'rxjs';
import { User } from '../models/User';
import { UserServerResponse } from '../models/UserServerResponse';
import { Token } from '../models/Token';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class UserService {
  loggedInStatus: Observable<boolean>;
  loggedSubject: Subject<boolean>;

  private loggedIn = false;
  private userId: string;
  private stacks = ["Web Fundamentals", "Python", "C#", "Java", "MEAN"];

  constructor(private _http: HttpClient) {
    this.loggedSubject = new Subject<boolean>();
    this.loggedInStatus = this.loggedSubject.asObservable();
  }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }
  
  setLoggedInStatus(isLoggedIn: boolean) : void {
    this.loggedSubject.next(isLoggedIn);
    this.loggedIn = isLoggedIn;
    if (isLoggedIn) {
      this.setUserId()
        .subscribe((val) => {
          this.userId = val;
        });
    }
  }

  setUserId() : Observable<string> {
    return this._http.get<string>('/profile/userid');
  }

  getUserId() : string | null {
    return this.userId;
  }

  getStacks(): string[] {
    return this.stacks;
  }

  registerUser(user: User): Observable<Token> {
    return this._http.post<Token>('/register', user);
  }

  loginUser(user: User): Observable<Token> {
    return this._http.post<Token>('/login', user);
  }

  getUserInfo(): Observable<UserServerResponse> {    
    return this._http.get<UserServerResponse>("/profile");
  }

  updateUser(user: User): Observable<UserServerResponse> {
    return this._http.post<UserServerResponse>("/profile/update", user);
  }
}
