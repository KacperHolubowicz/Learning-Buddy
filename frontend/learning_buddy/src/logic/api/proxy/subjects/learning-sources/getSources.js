import axiosApi from "../../../axios";

export default async function getSources(subjectId, privateSources, page) {
    return await axiosApi.get(`subject/${subjectId}/learning-source${privateSources ? "/private" : ""}?pageNumber=${page}`);
}