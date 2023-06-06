import axios from "../../axios";

export default async function getSubject(id) {
    return await axios.get(`subject/${id}`);
}