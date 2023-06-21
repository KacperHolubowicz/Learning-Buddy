import { useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import LoudButton from "../../atoms/LoudButton";
import { useParams, useNavigate } from "react-router-dom";
import editSource from "../../logic/api/proxy/subjects/learning-sources/editSource";
import getSource from "../../logic/api/proxy/subjects/learning-sources/getSource";
import Wrapper from "../../atoms/Wrapper";
import { useEffect } from "react";

function LearningSourceEditPage() {
    let [name, setName] = useState("");
    let [description, setDescription] = useState("");
    let [sourceType, setSourceType] = useState(1);
    const { learningSourceId } = useParams();
    const navigate = useNavigate();

    async function editLearningSource() {
        await editSource(learningSourceId, name, description, sourceType)
            .catch((err) => {
                console.log(err);
            });
        navigate(-1);
    }

    async function fetchSource(e) {
        e.preventDefault();
        await getSource(learningSourceId)
            .then((resp) => {
                setName(resp?.data?.name);
                setDescription(resp?.data?.description);
                setSourceType(resp?.data?.type);
            })
            .catch((err) => {
                console.log(err);
            })
    }

    useEffect(() => {
        fetchSource();
    }, []);

    return (
        <Container className="mt-3">
            <Wrapper>
                <form>
                    <Row className="mt-2 pe-3 px-3">
                        <Col>
                            <h3>Learning source name</h3>
                        </Col>
                        <Col>
                            <input type="text" value={name} onChange={(e) => setName(e.target.value)}/>
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Learning source description</h3>
                        </Col>
                        <Col>
                            <input type="text" value={description} onChange={(e) => setDescription(e.target.value)}/>
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Learning source type</h3>
                        </Col>
                        <Col>
                            <select onChange={(e) => setSourceType(e.target.value)}>
                                <option selected={sourceType === 0} value={0}>Book</option>
                                <option selected={sourceType === 1} value={1}>Website URL</option>
                                <option selected={sourceType === 2} value={2}>Video URL</option>
                            </select>
                        </Col>
                    </Row>
                    <Row className="mt-5 pb-3 pe-3 px-3">
                        <Col>
                            <LoudButton submit={true} text="Confirm" action={(e) => editLearningSource(e)} />
                        </Col>
                    </Row>
                </form>
            </Wrapper>
        </Container>
    )
}

export default LearningSourceEditPage;