import axios from "../../axios";

export default async function loginRequest(login, password) {
    return await axios.post('login', JSON.stringify({login, password}));
}