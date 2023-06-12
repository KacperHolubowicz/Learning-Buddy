import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import getSubject from "../logic/api/proxy/subjects/getSubject";
import { useNavigate } from "react-router-dom";
import {Container, Row, Col} from "react-bootstrap";
import Wrapper from "../atoms/Wrapper";
import QuietButton from "../atoms/QuietButton";
import SubjectDetails from "../organisms/SubjectDetails";
import getSourcesPreview from "../logic/api/proxy/subjects/getSourcesPreview";
import getTasksPreview from "../logic/api/proxy/subjects/getTasksPreview";
import SourcePreviewListElement from "../molecules/SourcePreviewListElement";
import NormalButton from "../atoms/NormalButton";
import LoudButton from "../atoms/LoudButton";

function SubjectPage() {
    let { id } = useParams();
    let [subject, setSubject] = useState({});
    let [sources, setSources] = useState([]);
    let [privateSourcesOption, setPrivateSourcesOption] = useState(false);
    let [tasks, setTasks] = useState([]);
    let [quizzes, setQuizzes] = useState([]);
    const navigate = useNavigate();

    async function fetchSubject() {
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

    async function fetchSources() {
        await getSourcesPreview(id, privateSourcesOption)
            .then((resp) => {
                console.log(resp);
                setSources(resp?.data)
            })
            .catch((err) => {
                console.log(err);
            })
    }

    async function fetchTasks() {
        await getTasksPreview(id)
            .then((resp) => {
                console.log(resp);
                setSources(resp?.data)
            })
            .catch((err) => {
                console.log(err);
            })
    }

    useEffect(() => {
        fetchSubject();
    }, [])

    useEffect(() => {
        fetchSources();
    }, [privateSourcesOption])

    return (
        <Container className="mt-3" fluid>
            <Row>
                <Col xs={3} className="d-flex justify-content-center">
                    <Wrapper height="550px" width="460px">
                        <Row className="mt-4 mb-4">
                            <h2 className="d-flex justify-content-center">This subject's learning sources</h2>
                        </Row>
                        <Row>
                            <Col className="d-flex justify-content-center">
                                <QuietButton text="Public" action={() => console.log()}/>
                            </Col>
                            <Col className="d-flex justify-content-center">
                                <QuietButton text="Private" action={() => console.log()}/>
                            </Col>
                        </Row>
                        {
                            sources?.paginatedProperty !== undefined ?
                            sources?.paginatedProperty.length === 0 ?
                            <Row className="mt-3 mb-3">
                                <h2 className="d-flex justify-content-center">No sources for this subject</h2> 
                            </Row>
                            :
                            sources.paginatedProperty.slice(0, 3).map((source, i) => (
                                <Row>
                                    <SourcePreviewListElement source={source} key={i} />
                                </Row>
                            )) :
                            "Loading"
                        }
                        <Row className="mt-2">
                            <Col className="d-flex justify-content-center">
                                <NormalButton text="See all..." action={() => console.log()} />
                            </Col>
                            <Col className="d-flex justify-content-center">
                                <LoudButton text="Add new" action={() => console.log()} />
                            </Col>
                        </Row>
                    </Wrapper>
                </Col>
                <Col xs={6} className="d-flex justify-content-center">
                    <SubjectDetails subject={subject} />
                </Col>
                <Col xs={3} className="d-flex justify-content-center">
                    <Wrapper height="550px" width="460px">

                    </Wrapper>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Wrapper height="300px" width="1880px">

                    </Wrapper>
                </Col>
            </Row>
        </Container>
    );
}

export default SubjectPage;