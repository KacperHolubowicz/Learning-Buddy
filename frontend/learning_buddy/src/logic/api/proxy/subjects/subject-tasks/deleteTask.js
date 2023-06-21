import axiosApi from "../../../axios";

export default async function deleteTask(subjectTaskId) {
    return await axiosApi.delete(`task/${subjectTaskId}`);
}