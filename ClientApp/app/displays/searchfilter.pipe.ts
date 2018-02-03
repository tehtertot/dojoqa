import { Pipe, PipeTransform } from '@angular/core';
import { QuestionServerResponse } from '../models/QuestionServerResponse';

import * as Fuse from 'fuse.js';
import { Tag } from '../models/Tag';
import { forEach } from '@angular/router/src/utils/collection';

@Pipe({
    name: 'searchfilter'
})
export class SearchFilterPipe implements PipeTransform {
    transform(value: Array<QuestionServerResponse>, searchStr: string, searchTags: string[]): Array<QuestionServerResponse> {
        if (!value || searchStr == "") { return value; }
        console.log(`searchtags are: ${searchTags}`);
        var options = { keys: ['questionText', 'questionTitle'] };

        var filteredQuestions;
        //filtering for questions only with ALL specified tags
        // if (searchTags.length > 0) {
        //     filteredQuestions = value.filter(question => {
        //         let tagValues = this.getFields(question.tags);
        //         for (let stag of searchTags) {
        //             if (tagValues.indexOf(stag) < 0) {
        //                 return false;
        //             }
        //         }
        //         return true;
        //     });
        // }
        // filtering for questions with ANY of the specified tags
        if (searchTags.length > 0) {
            filteredQuestions = value.filter(question => {
                let tagValues = this.getFields(question.tags);
                console.log(tagValues);
                for (let stag of tagValues) {
                    console.log(searchTags.indexOf(stag));
                    // console.log(`question: ${question.questionText}, ${tagValues.indexOf(stag)}`);
                    if (searchTags.indexOf(stag) >= 0) {
                        return true;
                    }
                }
                return false;
            });
        }
        else {
            filteredQuestions = value;
        }

        var fuse = new Fuse(filteredQuestions, options);
        // let filteredQuestions = fuse.search(searchStr);
        return fuse.search(searchStr);
        // return filteredQuestions;
    }

    private getFields(input: Tag[]) {
        let output: string[] = [];
        for (let i = 0; i < input.length; i++) {
            output.push(input[i].name);
        }
        return output;
    }
}