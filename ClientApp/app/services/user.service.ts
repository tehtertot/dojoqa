import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { User } from '../models/User';
import 'rxjs';

@Injectable()
export class UserService {

  constructor(private _http: Http) { }

  registerUser(user: User) {
    return this._http.post('/register', user).map(data => data.json()).toPromise();
  }
}
