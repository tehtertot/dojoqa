import { SimpleTag } from "./SimpleTag";

export class QuestionServerResponse {
    questionId: number;
    questionText: string;
    askedByFirstName: string;
    askedByLastName: string;
    askedById: string;
    createdAt: Date;
    tags: SimpleTag[];
}