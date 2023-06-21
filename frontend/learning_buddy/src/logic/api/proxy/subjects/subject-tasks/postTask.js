import axiosApi from "../../../axios";

export default async function postTask(subjectId, name, description, priority, difficulty, deadline) {
    return await axiosApi.post(`subject/${subjectId}/task`, JSON.stringify({
        name: name,
        description: description,
        priority: Number(priority),
        difficulty: Number(difficulty),
        deadline: deadline
    }));
}