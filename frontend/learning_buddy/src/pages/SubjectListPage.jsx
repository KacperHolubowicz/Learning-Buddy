import { useEffect, useState } from "react";
import getSubjects from "../logic/api/proxy/subjects/getSubjects";
import SubjectListElement from "../molecules/SubjectListElement";
import Stack from "react-bootstrap/Stack";

function SubjectListPage() {
    let [subjects, setSubjects] = useState([]);

    useEffect(() => {
        getSubjects()
            .then((resp) => {
                console.log(resp);
                setSubjects(resp?.paginatedProperty);
            })
            //error handling, like showing error page
    }, []);

    return (
        <Stack gap={2} className="d-flex align-items-center">
            {
                subjects !== null ?
                subjects.length === 0 ?
                <h1>No subjects on the list</h1> :
                subjects.map((subject, i) => (
                    <SubjectListElement subject={subject} key={i} />
                )) :
                "Loading"
            }
        </Stack>
    );
}

export default SubjectListPage;