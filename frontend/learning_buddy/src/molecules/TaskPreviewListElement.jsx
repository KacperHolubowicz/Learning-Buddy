import ListElementBackground from "../atoms/ListElementBackground";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";
import {useNavigate} from "react-router-dom";

function TaskPreviewListElement({task, id}) {
    const navigate = useNavigate();
    const styling = {
        display: "flex",
        marginTop: "15px",
        height: "80px"
    }

    return (
        <div style={styling} key={id}>
            <ListElementBackground>
                <ListElementItem text={task.name}/>
                <ListElementItem text={task.finished ? "✓" : "X"} />
                <div style={{display: "flex", marginLeft: "auto"}}>
                    {
                        !task.finished?
                        <ListElementOperation text="Edit" action={() => navigate(`/subject-tasks/${id}/edit`)}/> :
                        ""
                    }
                    <ListElementOperation text="Delete" action={() => navigate(`/subject-tasks/${id}/delete`)}/> 
                </div>
            </ListElementBackground>
        </div>
    )
}

export default TaskPreviewListElement;