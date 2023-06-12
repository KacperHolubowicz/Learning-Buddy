import axiosApi from "../../axios";

export default async function getTasksPreview(id) {
    return await axiosApi.get(`subject/${id}/task`);
}