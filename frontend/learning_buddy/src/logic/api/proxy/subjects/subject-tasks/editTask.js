import axiosApi from "../../../axios";

export default async function editTask(subjectTaskId, name, description, priority, difficulty, deadline, finished=false) {
    return await axiosApi.put(`task/${subjectTaskId}`, JSON.stringify({
        name: name,
        description: description,
        priority: Number(priority),
        difficulty: Number(difficulty),
        deadline: deadline,
        finished: finished
    }));
}