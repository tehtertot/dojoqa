import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { EqualValidator } from './models/equal-validator.directive';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from './components/profile/profile.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { SearchComponent } from './components/search/search.component';
import { LogoutComponent } from './components/home/logout.component';

import { UserService } from './services/user.service';
import { QuestionService } from './services/question.service';
import { AuthGuard } from './services/auth.guard';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        SearchComponent,
        FetchDataComponent,
        HomeComponent,
        ProfileComponent,
        LogoutComponent,
        EqualValidator
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
            { path: 'search', component: SearchComponent, canActivate: [AuthGuard] },
            { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard] },
            { path: 'logout', component: LogoutComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        UserService,
        QuestionService,
        AuthGuard,
    ]
})
export class AppModuleShared {
}
