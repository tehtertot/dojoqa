import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/User';

import { UserService } from '../../services/user.service';

@Component({
    selector: 'logout',
    template: '../home/home.component.html'
})

export class LogoutComponent {
    
    constructor(private _userService: UserService, private _router: Router) { }

    ngOnInit() {
        this._userService.setLoggedInStatus(false);
        this._router.navigate(['/home']);
    }
    
}
