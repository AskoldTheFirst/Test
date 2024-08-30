
export class AppGlobalStateEntity {

    public constructor() {
        this.whichTestIsRunningNow = null;
        this.currentUser = null;
    }

    public whichTestIsRunningNow: RunningTest | null;

    public currentUser: string | null;
}

export class RunningTest {

    public constructor(name: string) {
        this.name = name;
        this.currentQuestionNumber = 0;
    }

    public name: string;

    public currentQuestionNumber: number;
}