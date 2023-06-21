import { useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import LoudButton from "../../atoms/LoudButton";
import { useParams, useNavigate } from "react-router-dom";
import postTask from "../../logic/api/proxy/subjects/subject-tasks/postTask";
import Wrapper from "../../atoms/Wrapper";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

function SubjectTaskCreatePage() {
    let [name, setName] = useState("");
    let [description, setDescription] = useState("");
    let [priority, setPriority] = useState(1);
    let [difficulty, setDifficulty] = useState(1);
    let [deadline, setDeadline] = useState(new Date());
    let [errorMessage, setErrorMessage] = useState("");
    const { subjectId } = useParams();
    const navigate = useNavigate();

    async function createSubjectTask(e) {
        e.preventDefault();
        await postTask(subjectId, name, description, priority, difficulty, deadline)
            .catch((err) => {
                console.log(err);
            });
        navigate(`/subjects/${subjectId}/subject-tasks`);
    }

    function validateDate(date) {
        const dateNow = new Date().getTime()
        const dateProvided = new Date(date).getTime();
        if(dateNow >= dateProvided) {
            setErrorMessage("The deadline must be set in the future");
        } else {
            setDeadline(date);
            setErrorMessage("");
        }
    }

    return (
        <Container className="mt-3">
            <Wrapper>
                <form>
                    {
                        errorMessage !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {errorMessage}
                            </div>
                        </Row> :
                        ""
                    }
                    <Row className="mt-2 pe-3 px-3">
                        <Col>
                            <h3>Task name</h3>
                        </Col>
                        <Col>
                            <input type="text" required onChange={(e) => setName(e.target.value)}/>
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task description</h3>
                        </Col>
                        <Col>
                            <input type="text" onChange={(e) => setDescription(e.target.value)}/>
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task priority (1-5)</h3>
                        </Col>
                        <Col>
                            <input type="number" text="Priority" required value={priority} min="1" max="5" onChange={(e) => setPriority(e.target.value)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task difficulty (1-5)</h3>
                        </Col>
                        <Col>
                            <input type="number" text="Difficulty" required value={difficulty} min="1" max="5" onChange={(e) => setDifficulty(e.target.value)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task deadline</h3>
                        </Col>
                        <Col>
                            <DatePicker
                                showIcon
                                dateFormat="d MMMM, yyyy HH:mm"
                                timeFormat="HH:mm"
                                minDate={new Date()}
                                showTimeSelect
                                required
                                selected={deadline}
                                onChange={(date) => validateDate(date)}
                            />
                        </Col>
                    </Row>
                    <Row className="mt-5 pb-3 pe-3 px-3">
                        <Col>
                            <LoudButton disable={errorMessage !== ""} text="Create" action={(e) => createSubjectTask(e)} />
                        </Col>
                    </Row>
                </form>
            </Wrapper>
        </Container>
    )
}

export default SubjectTaskCreatePage;