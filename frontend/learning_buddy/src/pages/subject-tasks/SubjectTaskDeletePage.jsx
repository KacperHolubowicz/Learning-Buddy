import { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import LoudButton from "../../atoms/LoudButton";
import { useParams, useNavigate } from "react-router-dom";
import Wrapper from "../../atoms/Wrapper";
import deleteTask from "../../logic/api/proxy/subjects/subject-tasks/deleteTask";
import getTask from "../../logic/api/proxy/subjects/subject-tasks/getTask";

function SubjectTaskDeletePage() {
    let [task, setTask] = useState({});
    const { subjectTaskId } = useParams();
    const navigate = useNavigate();
    const options = {
        weekday: "long",
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "numeric",
        minute: "numeric"
    };

    async function fetchTask() {
        await getTask(subjectTaskId)
            .then((resp) => {
                setTask(resp?.data);
            })
            .catch((err) => {
                console.log(err);
            })
    }

    async function deleteSubjectTask() {
        await deleteTask(subjectTaskId)
            .catch((err) => {
                console.log(err);
            });
        navigate(-1);
    }

    useEffect(() => {
        fetchTask();
    }, []);

    return (
        <Container className="mt-3">
            <Wrapper>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h2>Are you sure you want to delete this subject task?</h2>
                    </Col>
                </Row>
                <Row className="mt-5 pe-3 px-3">
                    <Col>
                        <h3>{task.name}</h3>
                    </Col>
                </Row>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h3>{task.description}</h3>
                    </Col>
                </Row>
                <Row className="mt-5 pe-3 px-3">
                    <Col>
                        <h3>{task.finished ? "Finished" : "Unfinished"}</h3>
                    </Col>
                </Row>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h3>Priority: {task.priority}</h3>
                    </Col>
                </Row>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h3>Difficulty: {task.difficulty}</h3>
                    </Col>
                </Row>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h3>Deadline:
                            {task.deadline !== undefined ? 
                            new Intl.DateTimeFormat("en-GB", options).format(Date.parse(task.deadline)) :
                            ""
                            }
                        </h3>
                    </Col>
                </Row>
                <Row className="mt-5 pb-3 pe-3 px-3">
                    <Col>
                        <LoudButton text="Delete" action={() => deleteSubjectTask()} />
                    </Col>
                </Row>
            </Wrapper>
        </Container>
    )
}

export default SubjectTaskDeletePage;