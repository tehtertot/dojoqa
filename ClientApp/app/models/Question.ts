import { Tag } from "./Tag";

export class Question {
    _id: number;
    QuestionTitle: string;
    QuestionText: string;
    AskedByUser: string;
    Tags: Array<Tag> = [];
}