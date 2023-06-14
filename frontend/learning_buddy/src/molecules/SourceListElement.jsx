import ListElementBackground from "../atoms/ListElementBackground";
import ListElementLongItem from "../atoms/ListElementLongItem";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";
import {useNavigate} from "react-router-dom";

function SourceListElement({source, priv}) {
    const navigate = useNavigate();
    const styling = {
        display: "flex",
        marginTop: "15px"
    }

    return (
        <div style={styling}>
            <ListElementBackground>
                <ListElementItem text={source.name} />
                <ListElementItem text={source.type} />
                <ListElementLongItem text={source.description} />
                {
                    source.isOwner || priv ? 
                    <div style={{display: "flex", marginLeft: "auto"}}>
                        <ListElementOperation text="Edit" action={() => navigate(`/learning-sources/${source.id}/edit`)}/>
                        <ListElementOperation text="Delete" action={() => navigate(`/learning-sources/${source.id}/delete`)}/> 
                    </div> : ""
                }
            </ListElementBackground>
        </div>
    )
}

export default SourceListElement;