import axios from "axios"

const axiosAuth = axios.create({
    baseURL: "https://localhost:7276/api/v1",
    headers: {
        'Content-Type': 'application/json'
    },
    withCredentials: true
})



export default axiosAuth;