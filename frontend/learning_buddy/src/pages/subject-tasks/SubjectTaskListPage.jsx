import {Stack, Row, Col} from "react-bootstrap";
import NormalButton from "../../atoms/NormalButton";
import { useEffect, useState } from "react";
import getTasks from "../../logic/api/proxy/subjects/getTasks";
import { useNavigate, useParams } from "react-router-dom";
import TaskListElement from "../../molecules/TaskListElement";
import PageFooter from "../../molecules/PageFooter";

function SubjectTaskListPage() {
    let [tasks, setTasks] = useState([]);
    let [page, setPage] = useState(1);
    let [pagination, setPagination] = useState({});
    const navigate = useNavigate();
    const { subjectId } = useParams();

    async function fetchData() {
        await getTasks(subjectId, page)
            .then((resp) => {
                console.log(resp);
                setTasks(resp?.data?.paginatedProperty);
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
    }, [page]);

    return (
        <Stack gap={2} className="d-flex align-items-center">
            <Row className="mt-3 mb-5">
                <Col>
                    <NormalButton text="Add new task" action={() => navigate("new")} />
                </Col>
            </Row>
            <Row>
                <h2>Your task for given subject</h2>
            </Row>
            {
                tasks !== null ?
                tasks.length === 0 ?
                <h1>No subject tasks on the list</h1> :
                tasks.map((task) => (
                    <Row className="mt-2">
                        <TaskListElement task={task} key={task.id}/>
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

export default SubjectTaskListPage;