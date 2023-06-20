import axiosApi from "../../../axios";

export default async function editSource(learningSourceId, name, description, type) {
    return await axiosApi.put(`learning-source/${learningSourceId}`, JSON.stringify({
        name: name,
        description: description,
        type: Number(type)
    }));
}