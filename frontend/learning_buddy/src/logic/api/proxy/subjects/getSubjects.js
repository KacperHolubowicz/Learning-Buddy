import axiosInstance from "../../axios";

export default async function getSubjects(nameQuery, page) {
    return await axiosInstance.get(`subject?name=${nameQuery}&pageNumber=${page}`)
}