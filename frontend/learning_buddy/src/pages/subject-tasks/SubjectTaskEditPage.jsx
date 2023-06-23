import { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import LoudButton from "../../atoms/LoudButton";
import { useParams, useNavigate } from "react-router-dom";
import getTask from "../../logic/api/proxy/subjects/subject-tasks/getTask";
import editTask from "../../logic/api/proxy/subjects/subject-tasks/editTask";
import Wrapper from "../../atoms/Wrapper";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

function SubjectTaskEditPage() {
    let [name, setName] = useState("");
    let [description, setDescription] = useState("");
    let [priority, setPriority] = useState(1);
    let [difficulty, setDifficulty] = useState(1);
    let [deadline, setDeadline] = useState(new Date());
    let [alreadyFinished, setAlreadyFinished] = useState(false);
    let [finished, setFinished] = useState(false);
    let [errorDeadlineMessage, setErrorDeadlineMessage] = useState("");
    let [errorPriorityMessage, setErrorPriorityMessage] = useState("");
    let [errorDifficultyMessage, setErrorDifficultyMessage] = useState("");
    let [errorNameMessage, setErrorNameMessage] = useState("");
    const { subjectTaskId } = useParams();
    const navigate = useNavigate();

    async function fetchTask() {
        await getTask(subjectTaskId)
            .then((resp) => {
                setName(resp?.data.name);
                setDescription(resp?.data.description);
                setPriority(resp?.data.priority);
                setDifficulty(resp?.data.difficulty);
                setDeadline(new Date(resp?.data.deadline));
                setFinished(resp?.data.finished);
                setAlreadyFinished(resp?.data.finished);
            })
    }

    async function editSubjectTask(e) {
        e.preventDefault();
        if(validateName()) {
            await editTask(subjectTaskId, name, description, priority, difficulty, deadline, finished)
                .catch((err) => {
                    console.log(err);
                });
            navigate(-1);
        }
    }

    useEffect(() => {
        fetchTask();
    }, []);

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

    function checkError() {
        return (errorDeadlineMessage !== "" || errorPriorityMessage !== "" || errorDifficultyMessage !== "");
    }

    return (
        <Container className="mt-3">
            <Wrapper>
                <form>
                    {
                        alreadyFinished ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-primary">
                                This subject task is finished. You can't edit it anymore.
                            </div>
                        </Row> :
                        ""
                    }
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
                            <input type="text" disabled={alreadyFinished} value={name} onChange={(e) => setName(e.target.value)}/>
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task description</h3>
                        </Col>
                        <Col>
                            <input type="text" disabled={alreadyFinished} value={description} onChange={(e) => setDescription(e.target.value)}/>
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task priority (1-5)</h3>
                        </Col>
                        <Col>
                            <input type="number" disabled={alreadyFinished} text="Priority" value={priority} min="1" max="5" onChange={(e) => validatePriority(e.target.value)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Task difficulty (1-5)</h3>
                        </Col>
                        <Col>
                            <input type="number" disabled={alreadyFinished} text="Difficulty" value={difficulty} min="1" max="5" onChange={(e) => validateDifficulty(e.target.value)} />
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
                                disabled={alreadyFinished}
                                required
                                selected={deadline}
                                onChange={(date) => validateDate(date)}
                            />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Finished?</h3>
                        </Col>
                        <Col>
                            <input type="checkbox" disabled={alreadyFinished} checked={finished ? 'checked' : ''} onChange={() => setFinished(!finished)}/>
                        </Col>
                    </Row>
                    <Row className="mt-5 pb-3 pe-3 px-3">
                        <Col>
                            <LoudButton text="Submit" disable={alreadyFinished} action={(e) => editSubjectTask(e)} />
                        </Col>
                    </Row>
                </form>
            </Wrapper>
        </Container>
    )
}

export default SubjectTaskEditPage;