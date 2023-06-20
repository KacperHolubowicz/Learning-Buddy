import axiosApi from "../../../axios";

export default async function deleteSource(learningSourceId) {
    return await axiosApi.delete(`learning-source/${learningSourceId}`);
}