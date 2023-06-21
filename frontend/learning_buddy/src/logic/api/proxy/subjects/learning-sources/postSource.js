import axiosApi from "../../../axios";

export default async function postSource(subjectId, name, description, pub, type) {
    return await axiosApi.post(`subject/${subjectId}/learning-source`, JSON.stringify({
        name: name,
        description: description,
        public: pub,
        type: Number(type)
    }));
}