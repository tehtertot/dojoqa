import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from '../../models/User';

import { UserService } from '../../services/user.service';

@Component({
    selector: 'profile',
    templateUrl: './profile.component.html',
})
export class ProfileComponent {
    user: User = new User();

    constructor(private _userService: UserService, private _router: Router) { }

    ngOnInit() {
        this._userService.getUserInfo()
            .subscribe(
                (userInfo) => {
                    this.user.FirstName = userInfo.firstName;
                    this.user.LastName = userInfo.lastName;
                    this.user.Email = userInfo.email;
                    this.user.Password = userInfo.password;
                    this.user.LinkedInAccountURL = userInfo.linkedInAccountURL;
                },
                (err) => console.log(err));
    }
}
