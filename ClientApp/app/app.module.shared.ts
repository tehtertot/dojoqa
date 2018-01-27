import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { EqualValidator } from './models/equal-validator.directive';
import { TruncatePipe } from './displays/truncate.pipe';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileComponent } from './components/profile/profile.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { SearchComponent } from './components/search/search.component';
import { QuestionsComponent } from './components/search/questions/questions.component';
import { AskComponent } from './components/search/ask/ask.component';
import { LogoutComponent } from './components/home/logout.component';

import { UserService } from './services/user.service';
import { QuestionService } from './services/question.service';
import { AuthGuard } from './services/auth.guard';
import { UserAuthInterceptor } from './services/userauth.interceptor';
// import { AngularMultiSelect } from 'angular2-multiselect-dropdown/multiselect.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        SearchComponent,
        FetchDataComponent,
        HomeComponent,
        ProfileComponent,
        LogoutComponent,
        QuestionsComponent,
        AskComponent,
        EqualValidator,
        TruncatePipe
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
            { path: 'search', component: SearchComponent, canActivate: [AuthGuard], children: [
                { path: 'questions', component: QuestionsComponent },
                { path: 'ask', component: AskComponent }
            ] },
            { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard] },
            { path: 'logout', component: LogoutComponent },
            { path: '**', redirectTo: 'home' }
        ]),
    ],
    providers: [
        UserService,
        QuestionService,
        AuthGuard,
        {provide: HTTP_INTERCEPTORS,
        useClass: UserAuthInterceptor,
        multi: true}
    ]
})
export class AppModuleShared {
}
