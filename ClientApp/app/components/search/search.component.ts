import { Component, OnInit } from '@angular/core';
import { Question } from '../../models/Question';
import { QuestionServerResponse } from '../../models/QuestionServerResponse';

import { QuestionService } from '../../services/question.service';
import { Router } from '@angular/router';

@Component({
    selector: 'search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent {        
}
