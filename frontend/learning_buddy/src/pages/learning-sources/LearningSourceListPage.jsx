import {Stack, Row, Col} from "react-bootstrap";
import NormalButton from "../../atoms/NormalButton";
import QuietButton from "../../atoms/QuietButton";
import { useEffect, useState } from "react";
import getSources from "../../logic/api/proxy/subjects/getSources";
import { useNavigate, useParams } from "react-router-dom";
import SourceListElement from "../../molecules/SourceListElement";
import PageFooter from "../../molecules/PageFooter";

function LearningSourceListPage() {
    let [sources, setSources] = useState([]);
    let [page, setPage] = useState(1);
    let [pagination, setPagination] = useState({});
    let [privateSources, setPrivateSources] = useState(false);
    const navigate = useNavigate();
    const { subjectId } = useParams();

    async function fetchData() {
        await getSources(subjectId, privateSources, page)
            .then((resp) => {
                console.log(resp);
                setSources(resp?.data?.paginatedProperty);
                setPagination({
                    page: resp?.data?.page,
                    hasNext: resp?.data?.hasNext,
                    hasPrev: resp?.data?.hasPrevious,
                    totalPages: resp?.data?.totalPages
                })
            })
            .catch((error) => console.log(error));
    }

    useEffect(() => {
        fetchData()
    }, [page, privateSources]);

    return (
        <Stack gap={2} className="d-flex align-items-center">
            <Row className="mt-3 mb-5">
                <Col>
                    <NormalButton text="Add new source" action={() => navigate("new")} />
                </Col>
                <Col>
                    <QuietButton text="Public sources" action={() => setPrivateSources(false)} />
                </Col>
                <Col>
                    <QuietButton text="Your sources" action={() => setPrivateSources(true)} />
                </Col>
            </Row>
            <Row>
                {
                    privateSources ?
                    <h2 className="d-flex justify-content-center">Your learning sources</h2> :
                    <h2 className="d-flex justify-content-center">Public learning sources</h2>
                }
            </Row>
            {
                sources !== null ?
                sources.length === 0 ?
                <h1>No learning sources on the list</h1> :
                sources.map((source) => (
                    <Row className="mt-2">
                        <SourceListElement source={source} key={source.id} priv={privateSources}/>
                    </Row>
                )) :
                "Loading"
            }
            <Row className="d-flex justify-content-center align-items-center">
                <PageFooter page={pagination.page} hasNext={pagination.hasNext} hasPrevious={pagination.hasPrev} totalPages={pagination.totalPages} setPage={setPage}/>
            </Row>
        </Stack>
    );
}

export default LearningSourceListPage;