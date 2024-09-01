import { TechnologyDto } from "../DTOs/TechnologyDto";

export interface GlobalStateEntity {
    currentTest: TechnologyDto;
    testId: number;
}