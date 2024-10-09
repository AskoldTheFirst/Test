import axios, { AxiosError, AxiosResponse } from "axios";
import { store } from "../App/configureStore";
import { router } from "../App/Routes";
import { PageFilterParamsDto } from "./DTOs/PageFilterParamsDto";
import { ProfileDto } from "./DTOs/ProfileDto";

//axios.defaults.baseURL = 'http://localhost:5001/api/';
axios.defaults.baseURL = import.meta.env.VITE_API_URL;
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config => {
    const token = store.getState().account.user?.token;
    if (token) config.headers.Authorization = `Bearer ${token}`;
    //window.Logger.log('just request', 'user');
    return config;
});

axios.interceptors.response.use(async response => {
    return response;
}, /*not 2xx responce range*/(error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;
    
    const user = store.getState().account.user;
    window.Logger.log('agent interceptor: ' + data.title, user ? user.login : null);

    switch (status) {
        case 400:
            break;
        case 401:
            break;
        case 404:
            break;
        case 500:
            router.navigate('/server-error', { state: { error: data } });
            break;
        default:
            break;
    }
    
    return Promise.reject(data);
});

const requests = {
    get: (url: string, params?: any) => axios.get(url, { params }).then(responseBody),
    post: (url: string, body: object) => axios.post(url, body).then(responseBody),
    put: (url: string, body: object) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
}

const App = {
    initState: () => requests.get(`App/InitState`),
    technologies: () => requests.get(`App/Technologies`),
    logger: () => requests.get(`app/logger`),
    tracer: () => requests.get(`app/tracer`),
    postMessage: (email: string, message: string) => requests.post(`app/message`, { email: email, message: message }),
}

const Test = {
    initiateNewTest: (technologyName: string) => requests.post(`test/initiate-new-test?techName=${technologyName}`, {}),
    answer: (testId: number, questionId: number, answerNumber: number) => requests.post(`test/answer?testId=${testId}&questionId=${questionId}&answerNumber=${answerNumber}`, {}),
    nextQuestion: (testId: number) => requests.get(`test/next-question?testId=${testId}`),
    nextQuestionState: (testId: number | null) => requests.get(`test/next-question-state?testId=${testId}`),
    result: (testId: number) => requests.get(`test/test-result?testId=${testId}`),
    complete: (testId: number) => requests.put(`test/complete-test?testId=${testId}`, {})
}

const Account = {
    login: (values: any) => requests.post('account/login', values),
    register: (values: any) => requests.post('account/register', values),
    currentUser: () => requests.get('account/currentUser'),
}

const Statistics = {
    page: (filter: PageFilterParamsDto) => requests.get('statistics/result-rows', filter),
}

const Profile = {
    save: (profile: ProfileDto) => requests.post('account/user-profile', profile),
    get: () => requests.get('account/user-profile'),
}

const agent = {
    App,
    Test,
    Account,
    Statistics,
    Profile
}

export default agent;