import ListElementBackground from "../atoms/ListElementBackground";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";
import {useNavigate} from "react-router-dom";

function SourcePreviewListElement({source, id, priv}) {
    const navigate = useNavigate();
    const styling = {
        display: "flex",
        marginTop: "15px",
        height: "80px"
    }

    return (
        <div style={styling} key={id}>
            <ListElementBackground>
                <ListElementItem text={source.name}/>
                {
                    source.isOwner || priv ? 
                    <div style={{display: "flex", marginLeft: "auto"}}>
                        <ListElementOperation text="Edit" action={() => navigate(`/learning-sources/${id}/edit`)}/>
                        <ListElementOperation text="Delete" action={() => navigate(`/learning-sources/${id}/delete`)}/> 
                    </div> :
                    <ListElementItem text={source.type} />
                }
            </ListElementBackground>
        </div>
    )
}

export default SourcePreviewListElement;