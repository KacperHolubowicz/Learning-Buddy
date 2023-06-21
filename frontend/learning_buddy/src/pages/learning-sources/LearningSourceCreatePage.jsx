import { useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import LoudButton from "../../atoms/LoudButton";
import { useParams, useNavigate } from "react-router-dom";
import postSource from "../../logic/api/proxy/subjects/learning-sources/postSource";
import Wrapper from "../../atoms/Wrapper";

function LearningSourceCreatePage() {
    let [name, setName] = useState("");
    let [description, setDescription] = useState("");
    let [sourcePublic, setSourcePublic] = useState(false);
    let [sourceType, setSourceType] = useState(1);
    const { subjectId } = useParams();
    const navigate = useNavigate();

    async function createLearningSource() {
        await postSource(subjectId, name, description, sourcePublic, sourceType)
            .catch((err) => {
                console.log(err);
            });
        navigate(`/subjects/${subjectId}/learning-sources`);
    }

    return (
        <Container className="mt-3">
            <Wrapper>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h3>Learning source name</h3>
                    </Col>
                    <Col>
                        <input type="text" onChange={(e) => setName(e.target.value)}/>
                    </Col>
                </Row>
                <Row className="mt-5 pe-3 px-3">
                    <Col>
                        <h3>Learning source description</h3>
                    </Col>
                    <Col>
                        <input type="text" onChange={(e) => setDescription(e.target.value)}/>
                    </Col>
                </Row>
                <Row className="mt-5 pe-3 px-3">
                    <Col>
                        <h3>Public</h3>
                    </Col>
                    <Col>
                        <input type="checkbox" onChange={() => setSourcePublic(!sourcePublic)}/>
                    </Col>
                </Row>
                <Row className="mt-5 pe-3 px-3">
                    <Col>
                        <h3>Learning source type</h3>
                    </Col>
                    <Col>
                        <select onChange={(e) => setSourceType(e.target.value)}>
                            <option value={0}>Book</option>
                            <option selected value={1}>Website URL</option>
                            <option value={2}>Video URL</option>
                        </select>
                    </Col>
                </Row>
                <Row className="mt-5 pb-3 pe-3 px-3">
                    <Col>
                        <LoudButton text="Create" action={() => createLearningSource()} />
                    </Col>
                </Row>
            </Wrapper>
        </Container>
    )
}

export default LearningSourceCreatePage;