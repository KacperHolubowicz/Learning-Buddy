import { useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import LoudButton from "../../atoms/LoudButton";
import { useParams, useNavigate } from "react-router-dom";
import postTask from "../../logic/api/proxy/subjects/subject-tasks/postTask";
import Wrapper from "../../atoms/Wrapper";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

function SubjectTaskCreatePage() {
    const startDeadlineDate = new Date().setDate(new Date().getDate() + 1);
    let [name, setName] = useState("");
    let [description, setDescription] = useState("");
    let [priority, setPriority] = useState(1);
    let [difficulty, setDifficulty] = useState(1);
    let [deadline, setDeadline] = useState(new Date(startDeadlineDate));
    let [errorDeadlineMessage, setErrorDeadlineMessage] = useState("");
    let [errorPriorityMessage, setErrorPriorityMessage] = useState("");
    let [errorDifficultyMessage, setErrorDifficultyMessage] = useState("");
    let [errorNameMessage, setErrorNameMessage] = useState("");
    const { subjectId } = useParams();
    const navigate = useNavigate();

    async function createSubjectTask(e) {
        e.preventDefault();
        if(validateName()) {
            await postTask(subjectId, name, description, priority, difficulty, deadline)
                .catch((err) => {
                    console.log(err);
                });
            navigate(`/subjects/${subjectId}/subject-tasks`);
        }
    }

    function validateName() {
        if(name === "") {
            setErrorNameMessage("Name must be provided");
            return false;
        }
        return true;
    }

    function validateDate(date) {
        const dateNow = new Date().getTime()
        const dateProvided = new Date(date).getTime();
        if(dateNow >= dateProvided) {
            setErrorDeadlineMessage("The deadline must be set in the future");
        } else {
            setDeadline(date);
            setErrorDeadlineMessage("");
        }
    }

    function validatePriority(pr) {
        if(pr <= 0 || pr > 5) {
            setErrorPriorityMessage("The priority value must be between 1 and 5");
        } else {
            setPriority(pr);
            setErrorPriorityMessage("");
        }
    }

    function validateDifficulty(diff) {
        if(diff <= 0 || diff > 5) {
            setErrorDifficultyMessage("The difficulty value must be between 1 and 5");
        } else {
            setDifficulty(diff);
            setErrorDifficultyMessage("");
        }
    }

    return (
        <Container className="mt-3">
            <Wrapper>
                <form>
                    {
                        errorDeadlineMessage !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {errorDeadlineMessage}
                            </div>
                        </Row> :
                        ""
                    }
                    {
                        errorPriorityMessage !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {errorPriorityMessage}
                            </div>
                        </Row> :
                        ""
                    }
                    {
                        errorDifficultyMessage !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {errorDifficultyMessage}
                            </div>
                        </Row> :
                        ""
                    }
                    {
                        errorNameMessage !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {errorNameMessage}
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
                            <input type="number" text="Priority" required value={priority} min="1" max="5" onChange={(e) => validatePriority(e.target.value)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task difficulty (1-5)</h3>
                        </Col>
                        <Col>
                            <input type="number" text="Difficulty" required value={difficulty} min="1" max="5" onChange={(e) => validateDifficulty(e.target.value)} />
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
                            <LoudButton text="Create" action={(e) => createSubjectTask(e)} />
                        </Col>
                    </Row>
                </form>
            </Wrapper>
        </Container>
    )
}

export default SubjectTaskCreatePage;