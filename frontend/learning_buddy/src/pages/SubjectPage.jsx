import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import getSubject from "../logic/api/proxy/subjects/getSubject";

function SubjectPage() {
    let { id } = useParams();
    let [subject, setSubject] = useState({});

    async function fetchData() {
        await getSubject(id)
            .then((resp) => {
                console.log(resp);
                setSubject(resp?.data)
            })
            .catch((err) => {
                console.log(err);
            })
    }

    useEffect(() => {
        fetchData();
    }, [])

    return (
        <h1>{subject.name}</h1>
    );
}

export default SubjectPage;