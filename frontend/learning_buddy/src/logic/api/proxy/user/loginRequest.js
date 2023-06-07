import axiosAuth from "../../authAxios";

export default async function loginRequest(login, password) {
    return await axiosAuth.post('login', JSON.stringify({login, password}));
}