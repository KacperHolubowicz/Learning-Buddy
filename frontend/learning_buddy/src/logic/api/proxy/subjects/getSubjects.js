import axiosApi from "../../axios";

export default async function getSubjects(nameQuery, page) {
    return await axiosApi.get(`subject?name=${nameQuery}&pageNumber=${page}`)
}