
export class AppGlobalStateEntity {

    public constructor() {
        this.whichTestIsRunningNow = null;
        this.currentUser = null;
    }

    public whichTestIsRunningNow: RunningTest | null;

    public currentUser: string | null;
}

class RunningTest {

    public constructor(name: string) {
        this.name = name;
    }

    private name: string;

    private currentQuestionNumber: number;
}