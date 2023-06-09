import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import getSubject from "../../logic/api/proxy/subjects/getSubject";
import {Container, Row, Col} from "react-bootstrap";
import SubjectDetails from "../../organisms/SubjectDetails";
import LearningSourcesPreview from "../../organisms/LearningSourcesPreview";
import SubjectTasksPreview from "../../organisms/SubjectTasksPreview";
import QuizzesPreview from "../../organisms/QuizzesPreview";

function SubjectPage() {
    let { subjectId } = useParams();
    let [subject, setSubject] = useState({});

    async function fetchSubject() {
        await getSubject(subjectId)
            .then((resp) => {
                console.log(resp);
                setSubject(resp?.data)
            })
            .catch((err) => {
                console.log(err);
            })
    }

    useEffect(() => {
        fetchSubject();
    }, [])



    return (
        <Container className="mt-2" fluid>
            <Row>
                <Col xs={3} className="d-flex justify-content-center">
                    <LearningSourcesPreview subjectId={subjectId} />
                </Col>
                <Col xs={6} className="d-flex justify-content-center">
                    <SubjectDetails subject={subject} />
                </Col>
                <Col xs={3} className="d-flex justify-content-center">
                    <SubjectTasksPreview subjectId={subjectId} />
                </Col>
            </Row>
            <Row>
                <Col>
                    <QuizzesPreview subjectId={subjectId} />
                </Col>
            </Row>
        </Container>
    );
}

export default SubjectPage;