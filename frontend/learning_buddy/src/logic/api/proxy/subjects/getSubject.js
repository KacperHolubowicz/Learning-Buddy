import axiosApi from "../../axios";

export default async function getSubject(id) {
    return await axiosApi.get(`subject/${id}`);
}