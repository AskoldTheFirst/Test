import { Filter } from "../Entities/Filter";

export interface PageFilterParamsDto extends Filter {
    pageNumber: number;
    pageSize: number;
}