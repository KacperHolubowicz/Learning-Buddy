import axiosApi from "../../../axios";

export default async function getTasks(subjectId, page) {
    return await axiosApi.get(`subject/${subjectId}/task?pageNumber=${page}`);
}