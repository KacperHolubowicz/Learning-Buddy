import axios from "axios"

export default axios.create({
    baseURL: "https://localhost:7276/api/v1",
    headers: {
        'Content-Type': 'application/json'
    },
    withCredentials: true
})