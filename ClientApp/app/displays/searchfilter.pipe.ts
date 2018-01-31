import { Pipe, PipeTransform } from '@angular/core';
import { QuestionServerResponse } from '../models/QuestionServerResponse';

import * as Fuse from 'fuse.js';

@Pipe({
    name: 'searchfilter'
})
export class SearchFilterPipe implements PipeTransform {
    transform(value: Array<QuestionServerResponse>, searchStr: string): Array<QuestionServerResponse> {
        if (!value || searchStr == "") { return value; }

        var options = {
            keys: ['questionText']
        };

        var fuse = new Fuse(value, options);
        return fuse.search(searchStr);
    }
}