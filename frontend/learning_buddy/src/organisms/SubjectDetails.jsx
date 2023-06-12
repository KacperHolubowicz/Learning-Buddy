import Wrapper from "../atoms/Wrapper";
import {Row, Col} from "react-bootstrap";
import ListElementImage from "../atoms/ListElementImage";

function SubjectDetails({subject}) {
    return (
        <Wrapper height="550px" width="920px">
            <Row>
                <Col className="m-2" xs={3}>
                    <ListElementImage image={subject.thumbnail} width="200px" height="200px" alt="Thumbnail" />
                </Col>
                <Col xs={8} className="d-flex justify-content-center align-items-center">
                    <h1>{subject.name}</h1>
                </Col>
            </Row>
            <Row className="mt-5">
                <h1 />
            </Row>
            <Row className="mt-5">
                <Col className="m-2" xs={5}>
                    <h2>{subject.finished ? "Subject finished" : "Subject not finished"}</h2>
                </Col>
                <Col xs={6} className="ms-5 d-flex justify-content-center align-items-center">
                    <p>
                        {
                        subject.description !== null ? 
                        subject.description : 
                        "No description provided"}
                    </p>
                </Col>
            </Row>
            <Row className="mt-5">
                <h2 className="m-2">Creator: {subject.creatorUsername}</h2>
            </Row>
        </Wrapper>
    );
}

export default SubjectDetails;