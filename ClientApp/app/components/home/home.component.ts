import { Component } from '@angular/core';
import { User } from '../../models/User';

import { UserService } from '../../services/user.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent {
    user: User = new User();

    constructor(private _userService: UserService) { }

    register() {
        this._userService.registerUser(this.user)
            .then((res) => { 
                alert("success!".concat(res));
                this.user = new User();
            })
            .catch((err) => { 
                alert("failure".concat(err)); 
            });
    }
}
