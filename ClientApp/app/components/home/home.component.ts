import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../../models/User';

import { UserService } from '../../services/user.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {
    email = new FormControl();
    user: User = new User();
    errors: String = "";
    private location: string[];

    login: User = new User();
    loginerrors: String = "";

    constructor(private _userService: UserService, private _router: Router) { }

    ngOnInit() {
        this.location = ["Seattle", "San Jose", "Chicago", "Tulsa", "Houston", "LA", "Online"];
    }

    register() {
        this._userService.registerUser(this.user)
            .subscribe(
                (u) => this.successfulRedirect(u.auth_token), 
                (err) => console.log(err), 
                () => console.log("complete"));
    }

    userlogin() {
        this._userService.loginUser(this.login) 
            .subscribe(
                (u) => this.successfulRedirect(u.auth_token), 
                (err) => console.log(err), 
                () => console.log("complete"));
    }

    successfulRedirect(token: string) {
        this.user = new User();
        localStorage.setItem('auth_token', token);
        this._userService.setLoggedInStatus(true);
        this._router.navigate(['/search/questions']);
    }
}
