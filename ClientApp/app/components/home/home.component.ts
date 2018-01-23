import { Component } from '@angular/core';
import { User } from '../../models/User';

import { UserService } from '../../services/user.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent {
    user: User = new User();
    errors: String = "";

    login: User = new User();
    loginerrors: String = "";

    constructor(private _userService: UserService) { }

    register() {
        this._userService.registerUser(this.user)
            .then((res) => { 
                this.errors = res;
                this.user = new User();
            })
            .catch((err) => { 
                this.errors = err;
            });
    }

    userlogin() {
        this._userService.loginUser(this.login) 
            .then((res) => {
                console.log("success!!!");
                console.log(res);
            })
            .catch((err) => {
                this.loginerrors = err.json()["login"][0];
            })
    }
}
