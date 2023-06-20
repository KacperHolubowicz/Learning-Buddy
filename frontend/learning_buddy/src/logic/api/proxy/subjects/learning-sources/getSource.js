import axiosApi from "../../../axios";

export default async function getSource(learningSourceId) {
    return await axiosApi.get(`learning-source/${learningSourceId}`);
}