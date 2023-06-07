import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import getSubject from "../logic/api/proxy/subjects/getSubject";
import { useNavigate } from "react-router-dom";

function SubjectPage() {
    let { id } = useParams();
    let [subject, setSubject] = useState({});
    const navigate = useNavigate();

    async function fetchData() {
        await getSubject(id)
            .then((resp) => {
                console.log(resp);
                setSubject(resp?.data)
            })
            .catch((err) => {
                console.log(err);
                if(err.response.status === 404) {
                    navigate("/not-found");
                }
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