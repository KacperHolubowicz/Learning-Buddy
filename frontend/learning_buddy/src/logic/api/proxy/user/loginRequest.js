import axiosInstance from "../../axios";

export default async function loginRequest(login, password) {
    return await axiosInstance.post('login', JSON.stringify({login, password}));
}