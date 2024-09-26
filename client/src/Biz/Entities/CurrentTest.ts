import { QuestionDto } from "../DTOs/QuestionDto";

export interface CurrentTest {
    testId: number;
    totalAmount: number;
    questionNumber: number;
    question: QuestionDto | null;
    secondsLeft: number;
    technologyName: string;
}