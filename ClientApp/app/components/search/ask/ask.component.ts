import { Component, OnInit } from '@angular/core';
import { Question } from '../../../models/Question';
import { QuestionServerResponse } from '../../../models/QuestionServerResponse';

import { QuestionService } from '../../../services/question.service';
import { Router } from '@angular/router';
import { Tag } from '../../../models/Tag';

@Component({
    selector: 'ask',
    templateUrl: './ask.component.html',
    styleUrls: ['./ask.component.css']
})
export class AskComponent implements OnInit {
    public question: Question = new Question();
    public tagList: Array<Tag> = [];
    public selectedTags: Array<Tag> =[];
    public dropdownSettings = {};
    public dropdownList: Array<Object>;

    constructor(private _questionService: QuestionService, private _router: Router) { }
            
    ngOnInit() {
        this._questionService.getAllTags()
            .subscribe((tags) => {
                this.tagList = tags;
            });
        this.dropdownSettings = { 
            singleSelection: false, 
            text:"Select Tags",
            selectAllText:'Select All',
            unSelectAllText:'UnSelect All',
            enableSearchFilter: true,
            limitSelection: 4,
            enableCheckAll: false,
            searchAutofocus: true
            };       
        
    }

    public addQuestion() {
        if (this.question.QuestionText.trim() == "") {
            this.question.QuestionText = "";
            window.alert("Question cannot be empty");
        }
        else {
            this.question.QuestionText = this.question.QuestionText.trim();
            this._questionService.addQuestion(this.question)
                .subscribe((res) => {
                    this._router.navigate(['/search/questions']);
                })
        }
    }

    onItemSelect(item:Tag) {
        console.log(this.selectedTags);
    }
    OnItemDeSelect(item:Tag){
        console.log(this.selectedTags);
    }
    onSelectAll(items: any){
        console.log(items);
    }
    onDeSelectAll(items: any){
        console.log(items);
    }
        
}
