import { Tag } from "./Tag";

export class Question {
    _id: number;
    QuestionText: string;
    AskedByUser: string;
    Tags: Array<Tag>;
}