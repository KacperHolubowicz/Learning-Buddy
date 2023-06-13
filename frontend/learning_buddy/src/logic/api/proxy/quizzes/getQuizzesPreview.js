import axiosApi from "../../axios";

export default async function getQuizzesPreview(subjectId) {
    return await axiosApi.get(`subject/${subjectId}/quiz?pageSize=3&pageNumber=1`);
}