import { useEffect, useState } from "react";
import getSubjects from "../logic/api/proxy/subjects/getSubjects";
import SubjectListElement from "../molecules/SubjectListElement";
import Stack from "react-bootstrap/Stack";
import SubjectSearcher from "../molecules/SubjectSearcher";
import PageFooter from "../molecules/PageFooter";

function SubjectListPage() {
    let [subjects, setSubjects] = useState([]);
    let [search, setSearch] = useState("");
    let [page, setPage] = useState(1);
    let [pagination, setPagination] = useState({});

    async function fetchData() {
        await getSubjects(search, page)
        .then((resp) => {
            console.log(resp);
            setSubjects(resp?.data?.paginatedProperty);
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
        fetchData();
    }, [page]);

    return (
            <Stack gap={2} className="d-flex align-items-center">
                <SubjectSearcher search={search} setSearch={setSearch} searchAction={() => fetchData()}/>
                {
                    subjects !== null ?
                    subjects.length === 0 ?
                    <h1>No subjects on the list</h1> :
                    subjects.map((subject, i) => (
                        <SubjectListElement subject={subject} key={i} />
                    )) :
                    "Loading"
                }
                <PageFooter page={pagination.page} hasNext={pagination.hasNext} hasPrevious={pagination.hasPrev} totalPages={pagination.totalPages} setPage={setPage}/>
            </Stack>
    );
}

export default SubjectListPage;