import Wrapper from "../atoms/Wrapper";
import NormalButton from "../atoms/NormalButton";
import Stack from "react-bootstrap/esm/Stack";

function SubjectSearcher({searchAction, search, setSearch}) {
    return (
        <Wrapper height="200px" width="600px">
            <Stack gap={3} className="d-flex align-items-center">
                <h2>Subject search</h2>
                <input type="text" placeholder="Enter subject name" value={search} onChange={(e) => setSearch(e.target.value)}/>
                <NormalButton text="Search" action={searchAction}/>
            </Stack>
        </Wrapper>
    )
}

export default SubjectSearcher;