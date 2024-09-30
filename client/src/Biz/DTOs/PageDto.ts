import { TestRowDto } from "./TestRowDto";

export interface PageDto  {
    rows: TestRowDto[];
    total: number;
}