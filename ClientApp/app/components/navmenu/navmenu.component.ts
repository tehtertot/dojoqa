import { Component } from '@angular/core';

import { UserService } from '../../services/user.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    loggedIn: boolean;
    constructor(private _userService: UserService) {
        _userService.loggedInStatus.subscribe((status) => this.loggedIn = status );
    }
}
