import Wrapper from "../atoms/Wrapper";
import { Row, Col } from "react-bootstrap";
import TaskPreviewListElement from "../molecules/TaskPreviewListElement";
import NormalButton from "../atoms/NormalButton";
import LoudButton from "../atoms/LoudButton";
import getTasksPreview from "../logic/api/proxy/subjects/getTasksPreview";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

function SubjectTasksPreview({subjectId}) {
    let [tasks, setTasks] = useState([]);
    let [unauthenticated, setUnauthenticated] = useState(false);
    const navigate = useNavigate();

    async function fetchTasks() {
        await getTasksPreview(subjectId)
            .then((resp) => {
                console.log(resp);
                setTasks(resp?.data)
            })
            .catch((err) => {
                console.log(err);
                if(err.response.status === 401) {
                    setUnauthenticated(true);
                }
            })
    }

    useEffect(() => {
        fetchTasks();
    }, [])

    return (
        <Wrapper height="460px" width="460px">
            <Row className="mt-2 mb-2">
                <h2 className="d-flex justify-content-center">Your tasks for this subject</h2>
            </Row>
            {
                tasks?.paginatedProperty !== undefined ?
                tasks?.paginatedProperty.length === 0 ?
                <Row className="mt-2 mb-2">
                    <h2 className="d-flex justify-content-center">No tasks for this subject</h2> 
                </Row>
                :
                tasks.paginatedProperty.slice(0, 3).map((task) => (
                    <Row>
                        <TaskPreviewListElement key={task.id} task={task} id={task.id} />
                    </Row>
                )) :
                unauthenticated ?
                <h2 className="d-flex justify-content-center">You are not logged in</h2> :
                <h2 className="d-flex justify-content-center">Loading...</h2> 
            }
            <Row className="mt-2">
                <Col className="d-flex justify-content-center">
                    <NormalButton text="See all..." action={() => navigate("./subject-tasks")} />
                </Col>
                <Col className="d-flex justify-content-center">
                    <LoudButton text="Add new" action={() => navigate("./subject-tasks/new")} />
                </Col>
            </Row>
        </Wrapper>
    );
}

export default SubjectTasksPreview;