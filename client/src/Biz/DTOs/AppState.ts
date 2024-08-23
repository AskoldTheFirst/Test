export interface AppInitialState {
    tops: Top[];
}

export interface Top {
    technologyName: string;
    lines: TopLine[];
}

export interface TopLine {
    login: string;
    score: number;
    testDate: string;
}