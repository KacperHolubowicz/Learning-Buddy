import ListElementBackground from "../atoms/ListElementBackground";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";
import {useNavigate} from "react-router-dom";

function SourcePreviewListElement({source}) {
    const navigate = useNavigate();
    const styling = {
        display: "flex",
        marginTop: "15px",
        height: "80px"
    }

    return (
        <div style={styling}>
            <ListElementBackground>
                <ListElementItem text={source.name}/>
                {
                    source.isOwner ? 
                    <div style={{display: "flex", marginLeft: "auto"}}>
                        <ListElementOperation text="Edit" action={() => console.log("Edit")}/>
                        <ListElementOperation text="Delete" action={() => console.log("Delete")}/> 
                    </div> :
                    <ListElementItem text={source.type} />
                }
            </ListElementBackground>
        </div>
    )
}

export default SourcePreviewListElement;