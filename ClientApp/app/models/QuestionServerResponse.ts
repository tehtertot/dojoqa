import { Answer } from './Answer';

export class QuestionServerResponse {
    questionId: number;
    questionText: string;
    askedByFirstName: string;
    askedByLastName: string;
    askedById: string;
    createdAt: Date;
    answers: Answer[];
}