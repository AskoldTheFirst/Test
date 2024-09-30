export class Helper {

    static ConvertArrayToString(arr: number[], separator: string = ','): string {
        let str = '';
        for (let i = 0; i < arr.length; ++i) {
            str += arr[i].toString();
            str += separator;
        }

        return str;
    }

    static FromSecondsToMinAndSeconds(seconds: number): string {
        const sec = seconds % 60;
        const min = (seconds - sec) / 60;
        return `${min}:${sec}`;
    }
}