import axiosApi from "../../axios";

export default async function getSourcesPreview(id, privateSources) {
    return await axiosApi.get(`subject/${id}/learning-source${privateSources ? "/private" : ""}?pageSize=3&pageNumber=1`);
}