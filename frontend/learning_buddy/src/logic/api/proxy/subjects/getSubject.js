import axiosInstance from "../../axios";

export default async function getSubject(id) {
    return await axiosInstance.get(`subject/${id}`);
}