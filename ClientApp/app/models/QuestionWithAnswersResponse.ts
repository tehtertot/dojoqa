import { Answer } from './Answer';

export class QuestionWithAnswersResponse {
    questionId: number;
    questionText: string;
    askedByFirstName: string;
    askedByLastName: string;
    askedById: string;
    createdAt: Date;
    answers: Answer[] = [];
}