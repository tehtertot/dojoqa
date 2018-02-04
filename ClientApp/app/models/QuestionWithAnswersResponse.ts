import { Answer } from './Answer';

export class QuestionWithAnswersResponse {
    questionId: number;
    questionTitle: string;
    questionText: string;
    votes: number;
    canVote: boolean;
    askedByFirstName: string;
    askedByLastName: string;
    askedById: string;
    createdAt: Date;
    answers: Answer[] = [];
    tagsString: string[] = [];
}