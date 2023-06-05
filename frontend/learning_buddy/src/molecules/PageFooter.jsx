import NormalButton from "../atoms/NormalButton";
import LoudButton from "../atoms/LoudButton";

function PageFooter({hasNext, hasPrevious, page, setPage, totalPages}) {
    return (
        <div className="d-flex align-items-center mt-5 pt-5">
            { hasPrevious ? <NormalButton text="Previous" action={() => setPage(page - 1)}/> : "" }
            <b>Page {page}/{totalPages}</b>
            { hasNext ? <LoudButton text="Next" action={() => setPage(page + 1)}/> : "" }
        </div>
    );
}

export default PageFooter;