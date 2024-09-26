import { QuestionDto } from "./QuestionDto"

export interface NextQuestionStateDto {
    question: QuestionDto;
    questionNumber: number;
    totalAmount: number;
    secondsLeft: number;
}