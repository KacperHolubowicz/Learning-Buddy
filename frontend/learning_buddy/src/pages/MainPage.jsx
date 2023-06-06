import { Stack } from "react-bootstrap";
import useAuth from "../logic/hooks/useAuth";

function MainPage() {
    const { auth } = useAuth();
    return (
        <Stack gap={5} className="col-12 mx-auto">
            {
            auth?.username === undefined ?
                <>
                    <div>
                        <span />
                    </div>
                    <div className="d-flex justify-content-center">
                        <h1>Welcome to the LearningBuddy application!</h1>
                    </div>
                    <div>
                        <span />
                    </div>
                    <div className="d-flex justify-content-center">
                        <h2>This application allows you to learn different subject by solving quizzes!</h2>
                    </div>
                    <div>
                        <span />
                    </div>
                    <div className="d-flex justify-content-center">
                        <h3>You can browse public subjects, solve their quizzes and learn from their various learning sources without an account</h3>
                    </div>
                    <div>
                        <span />
                    </div>
                    <div className="d-flex justify-content-center">
                        <h3>If you want to create your own subjects or quizzes and save all of your quiz attempts - make sure to sign up.</h3>
                    </div>
                </>
                :
                <div className="d-flex justify-content-center">
                    <h1>Good to see you {auth.username}</h1>
                </div>
            }
        </Stack>
    )
}

export default MainPage;