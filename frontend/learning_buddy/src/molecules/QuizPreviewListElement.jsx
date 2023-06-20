import ListElementBackground from "../atoms/ListElementBackground";
import ListElementItem from "../atoms/ListElementItem";
import ListElementOperation from "../atoms/ListElementOperation";
import {useNavigate} from "react-router-dom";

function QuizPreviewListElement({quiz, id}) {
    const navigate = useNavigate();
    const options = {
        weekday: "long",
        year: "numeric",
        month: "long",
        day: "numeric",
    };
    const styling = {
        display: "flex",
        marginTop: "15px",
        height: "80px"
    }

    return (
        <div style={styling} key={id}>
            <ListElementBackground>
                <ListElementItem text={quiz.name}/>
                <ListElementItem text={quiz.questionCount + " questions"}/>
                <ListElementItem text={"Creator: " + quiz.userUsername}/>
                <ListElementItem text={"Last modified: " + new Intl.DateTimeFormat("en-GB", options).format(Date.parse(quiz.modifiedAt))} />
                {
                    quiz.isOwner ? 
                    <div style={{display: "flex", marginLeft: "auto"}}>
                        <ListElementOperation text="Edit" action={() => navigate(`quiz/${id}/edit`)}/>
                        <ListElementOperation text="Delete" action={() => navigate(`quiz/${id}/delete`)}/> 
                    </div> :
                    ""
                }
            </ListElementBackground>
        </div>
    )
}

export default QuizPreviewListElement;