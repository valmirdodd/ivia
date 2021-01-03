import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:5001/v1",
});

let contadorPost = 0;

api.interceptors.request.use(
    function (config) {
        if (config.method === "post") {
            ++contadorPost;
        }
        console.log(
            `${contadorPost} tentativas de inserir agenda`
        );
        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

export default api;
