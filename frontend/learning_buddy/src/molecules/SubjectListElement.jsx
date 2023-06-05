import ListElementBackground from "../atoms/ListElementBackground";
import ListElementImage from "../atoms/ListElementImage";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";

function SubjectListElement({subject}) {
    const styling = {
        display: "flex",
        marginTop: "15px"
    }

    return (
        <div style={styling}>
            <ListElementBackground>
                {
                    subject.thumbnail !== null ? 
                    <ListElementImage image={subject.thumbnail} width="200" height="200" alt="Thumbnail" /> : 
                    ""
                }
                <ListElementItem text={subject.name} />
                <ListElementItem  text={subject.finished ? "Finished" : "Unfinished"} />
                <ListElementOperation text="Enter" action={() => console.log("Enter")}/>
                {
                    subject.isOwner ? 
                    <div style={{display: "flex", marginLeft: "auto"}}>
                        <ListElementOperation text="Edit" action={() => console.log("Edit")}/>
                        <ListElementOperation text="Delete" action={() => console.log("Delete")}/> 
                    </div> : ""
                }
            </ListElementBackground>
        </div>
    )
}



export default SubjectListElement;