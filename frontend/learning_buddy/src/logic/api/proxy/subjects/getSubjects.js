import axios from "../../axios";

export default async function getSubjects(nameQuery, page) {
    const resp = await axios.get(`subject?name=${nameQuery}&pageNumber=${page}`)
        .catch((error) => console.log(error));
    return resp?.data;
}