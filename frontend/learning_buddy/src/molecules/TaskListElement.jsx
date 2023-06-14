import ListElementBackground from "../atoms/ListElementBackground";
import ListElementLongItem from "../atoms/ListElementLongItem";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";
import {useNavigate} from "react-router-dom";

function TaskListElement({task}) {
    const navigate = useNavigate();
    const options = {
        weekday: "long",
        year: "numeric",
        month: "long",
        day: "numeric",
    };
    const styling = {
        display: "flex",
        marginTop: "15px"
    }
    const nowDate = new Date().getTime();
    const deadline = new Date(task.deadline).getTime();
    const deadlineTextColor = deadline > nowDate ? "lime" : "red";

    return (
        <div style={styling}>
            <ListElementBackground>
                <ListElementItem text={task.name} />
                <ListElementItem text={task.finished ? "Finished" : "Unfinished"} />
                <ListElementLongItem text={task.description} />
                <ListElementItem text={"Priority: " + task.priority} />
                <ListElementItem text={"Difficulty: " + task.difficulty} />
                <ListElementItem textColor={deadlineTextColor} text={"Deadline: " + new Intl.DateTimeFormat("en-GB", options).format(Date.parse(task.deadline))} />
                <div style={{display: "flex", marginLeft: "auto"}}>
                    <ListElementOperation text="Edit" action={() => navigate(`/subject-tasks/${task.id}/edit`)}/>
                    <ListElementOperation text="Delete" action={() => navigate(`/subject-tasks/${task.id}/delete`)}/> 
                </div>
            </ListElementBackground>
        </div>
    )
}

export default TaskListElement;