import ListElementBackground from "../atoms/ListElementBackground";
import ListElementImage from "../atoms/ListElementImage";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";
import ListElementLongItem from "../atoms/ListElementLongItem";
import {useNavigate} from "react-router-dom";

function SubjectListElement({subject}) {
    const navigate = useNavigate();
    const styling = {
        display: "flex",
        marginTop: "15px"
    }

    return (
        <div style={styling}>
            <ListElementBackground>
                <ListElementImage image={subject.thumbnail} width="200" height="200" alt="Thumbnail" />
                <ListElementItem text={subject.name} />
                <ListElementItem  text={subject.finished ? "Finished" : "Unfinished"} />
                <ListElementLongItem text={subject?.description} />

                    <div style={{display: "flex", marginLeft: "auto"}}>
                        <ListElementOperation text="Enter" action={() => navigate(`./${subject.id}`)}/>
                        {
                            subject.isOwner ?
                            <>
                                <ListElementOperation text="Edit" action={() => console.log("Edit")}/>
                                <ListElementOperation text="Delete" action={() => console.log("Delete")}/>
                            </>
                            : ""
                        }
                    </div> 

            </ListElementBackground>
        </div>
    )
}

export default SubjectListElement;