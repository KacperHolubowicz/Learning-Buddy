import axiosApi from "../../../axios";

export default async function getTask(subjectTaskId) {
    return await axiosApi.get(`task/${subjectTaskId}`);
}