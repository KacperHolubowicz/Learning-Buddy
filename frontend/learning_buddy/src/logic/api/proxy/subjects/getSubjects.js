import axios from "../../axios";

export default async function getSubjects(nameQuery, page) {
    return await axios.get(`subject?name=${nameQuery}&pageNumber=${page}`)
}