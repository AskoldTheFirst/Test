import axios, { AxiosError, AxiosResponse } from "axios";

axios.defaults.baseURL = 'http://localhost:5001/api/';
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(async response => {
    return response;
}, /*not 2xx responce range*/ (error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;
    
    switch (status) {
        case 400:
            break;
        case 401:
        case 404:
            break;
        case 500:
            break;
        default:
            break;
    }
    return Promise.reject(error.response);
});

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: object) => axios.post(url, body).then(responseBody),
    put: (url: string, body: object) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
}

const App = {
    initState: () => requests.get(`App/InitState`),
    technologies: () => requests.get(`App/Technologies`),
    tops: (amount: number) => requests.get(`statistics/tops?topAmount=${amount}`)
}

const agent = {
    App
}

export default agent;