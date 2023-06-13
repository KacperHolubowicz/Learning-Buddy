import Wrapper from "../atoms/Wrapper";
import { Row, Col } from "react-bootstrap";
import QuizPreviewListElement from "../molecules/QuizPreviewListElement";
import NormalButton from "../atoms/NormalButton";
import LoudButton from "../atoms/LoudButton";
import { useState, useEffect } from "react";
import getQuizzesPreview from "../logic/api/proxy/quizzes/getQuizzesPreview";
import { useNavigate } from "react-router-dom";

function QuizzesPreview({subjectId}) {
    let [quizzes, setQuizzes] = useState([]);
    const navigate = useNavigate();

    async function fetchQuizzes() {
        await getQuizzesPreview(subjectId)
            .then((resp) => {
                console.log(resp);
                setQuizzes(resp?.data)
            })
            .catch((err) => {
                console.log(err);
            })
    }

    useEffect(() => {
        fetchQuizzes();
    }, [])

    return (
        <Wrapper height="400px" width="1880px">
            <Row className="mt-1 mb-1">
                <h2 className="d-flex justify-content-center">Quizzes for this subject</h2>
            </Row>
            {
                quizzes?.paginatedProperty !== undefined ?
                quizzes?.paginatedProperty.length === 0 ?
                <Row className="mt-2 mb-2">
                    <h2 className="d-flex justify-content-center">No quizzes for this subject</h2> 
                </Row>
                :
                quizzes.paginatedProperty.slice(0, 3).map((quiz) => (
                    <Row className="me-auto">
                        <div className="d-flex align-items-center justify-content-center">
                            <QuizPreviewListElement key={quiz.id} quiz={quiz} id={quiz.id} />
                        </div>
                    </Row>
                )) :
                <h2 className="d-flex justify-content-center">Loading...</h2> 
            }
            <Row className="mt-1">
                <Col className="d-flex justify-content-center">
                    <NormalButton text="See all..." action={() => navigate("./quizzes")} />
                </Col>
                <Col className="d-flex justify-content-center">
                    <LoudButton text="Add new" action={() => navigate("./quizzes/new")} />
                </Col>
            </Row>
        </Wrapper>
    );
}

export default QuizzesPreview;