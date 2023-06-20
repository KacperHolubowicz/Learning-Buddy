import { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import LoudButton from "../../atoms/LoudButton";
import { useParams, useNavigate } from "react-router-dom";
import Wrapper from "../../atoms/Wrapper";
import getSource from "../../logic/api/proxy/subjects/getSource";
import deleteSource from "../../logic/api/proxy/subjects/deleteSource";

function LearningSourceDeletePage() {
    let [source, setSource] = useState({});
    const { learningSourceId } = useParams();
    const navigate = useNavigate();

    async function fetchSource() {
        await getSource(learningSourceId)
            .then((resp) => {
                setSource(resp?.data);
            })
            .catch((err) => {
                console.log(err);
            })
    }

    async function deleteLearningSource() {
        await deleteSource(learningSourceId)
            .catch((err) => {
                console.log(err);
            });
        navigate(-1);
    }

    useEffect(() => {
        fetchSource();
    }, []);

    return (
        <Container className="mt-3">
            <Wrapper>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h2>Are you sure you want to delete this learning source?</h2>
                    </Col>
                </Row>
                <Row className="mt-5 pe-3 px-3">
                    <Col>
                        <h3>{source.name}</h3>
                    </Col>
                </Row>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h3>{source.description}</h3>
                    </Col>
                </Row>
                <Row className="mt-5 pe-3 px-3">
                    <Col>
                        <h3>{source.public ? "Public" : "Private"}</h3>
                    </Col>
                </Row>
                <Row className="mt-2 pe-3 px-3">
                    <Col>
                        <h3>{source.type === 0 ? "Book" : source.type === 1 ? "Website url" : "Video url"}</h3>
                    </Col>
                </Row>
                <Row className="mt-5 pb-3 pe-3 px-3">
                    <Col>
                        <LoudButton text="Delete" action={() => deleteLearningSource()} />
                    </Col>
                </Row>
            </Wrapper>
        </Container>
    )
}

export default LearningSourceDeletePage;