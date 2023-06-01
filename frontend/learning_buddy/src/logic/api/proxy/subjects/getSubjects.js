import axios from "../../axios";

export default async function getSubjects() {
    const resp = await axios.get('subject')
        .catch((error) => console.log(error));
    return resp?.data;
}