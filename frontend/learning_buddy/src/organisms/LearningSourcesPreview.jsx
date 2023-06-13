import Wrapper from "../atoms/Wrapper";
import QuietButton from "../atoms/QuietButton";
import SourcePreviewListElement from "../molecules/SourcePreviewListElement";
import { Row, Col } from "react-bootstrap";
import NormalButton from "../atoms/NormalButton";
import LoudButton from "../atoms/LoudButton";
import { useState, useEffect } from "react";
import getSourcesPreview from "../logic/api/proxy/subjects/getSourcesPreview";
import { useNavigate } from "react-router-dom";

function LearningSourcesPreview({subjectId}) {
    let [sources, setSources] = useState([]);
    let [privateSourcesOption, setPrivateSourcesOption] = useState(false);
    const navigate = useNavigate();

    async function fetchSources() {
        console.log(privateSourcesOption);
        await getSourcesPreview(subjectId, privateSourcesOption)
            .then((resp) => {
                console.log(resp);
                setSources(resp?.data)
            })
            .catch((err) => {
                console.log(err);
            })
    }

    useEffect(() => {
        fetchSources();
    }, [privateSourcesOption])

    return (
        <Wrapper height="460px" width="460px">
            <Row className="mt-2 mb-2">
                <h2 className="d-flex justify-content-center">This subject's learning sources</h2>
            </Row>
            <Row>
                <Col className="d-flex justify-content-center">
                    <QuietButton text="Public" action={() => setPrivateSourcesOption(false)}/>
                </Col>
                <Col className="d-flex justify-content-center">
                    <QuietButton text="Private" action={() => setPrivateSourcesOption(true)}/>
                </Col>
            </Row>
            {
                sources?.paginatedProperty !== undefined ?
                sources?.paginatedProperty.length === 0 ?
                <Row className="mt-2 mb-2">
                    <h2 className="d-flex justify-content-center">No sources for this subject</h2> 
                </Row>
                :
                sources?.paginatedProperty.slice(0, 3).map((source) => (
                    <Row>
                        <SourcePreviewListElement key={source.id} source={source} id={source.id} priv={privateSourcesOption} />
                    </Row>
                )) :
                "Loading"
            }
            <Row className="mt-2">
                <Col className="d-flex justify-content-center">
                    <NormalButton text="See all..." action={() => navigate('./learning-sources')} />
                </Col>
                <Col className="d-flex justify-content-center">
                    <LoudButton text="Add new" action={() => navigate('./learning-sources/new')} />
                </Col>
            </Row>
        </Wrapper>
    );
}

export default LearningSourcesPreview;