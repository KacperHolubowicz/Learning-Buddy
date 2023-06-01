import ListElementBackground from "../atoms/ListElementBackground";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";

function SubjectListElement({subject}) {
    return (
        <div style={styling}>
            <ListElementBackground>
                {
                    subject.thumbnail !== null ? 
                    <img src={`data:image/jpeg;base64,${subject.thumbnail}`} /> : 
                    ""
                }
                <ListElementItem text={subject.name} />
                <ListElementItem  text={subject.finished ? "Finished" : "Unfinished"} />
                {
                    !subject.isOwner ? 
                    <div style={{display: "inline-flex"}}>
                        <ListElementOperation text="Edit" action={() => console.log("Edit")}/>
                        <ListElementOperation text="Delete" action={() => console.log("Delete")}/> 
                    </div> : ""
                }
            </ListElementBackground>
        </div>
    )
}

const styling = {
    display: "block",
    marginTop: "10px",
    marginBottom: "10px"
}

export default SubjectListElement;