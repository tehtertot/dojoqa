import { Answer } from './Answer';
import { Tag } from './Tag';

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
    tags: Tag[] = [];
}