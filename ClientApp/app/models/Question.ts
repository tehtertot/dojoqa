import { Tag } from "./Tag";

export class Question {
    QuestionId: number;
    QuestionTitle: string;
    QuestionText: string;
    AskedByUser: string;
    Tags: Array<Tag> = [];
}