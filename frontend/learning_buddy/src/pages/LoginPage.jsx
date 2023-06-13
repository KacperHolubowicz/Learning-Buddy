import LoudButton from "../atoms/LoudButton";
import NormalButton from "../atoms/NormalButton";
import Wrapper from "../atoms/Wrapper";
import {Container, Row, Col} from "react-bootstrap";
import { useState } from "react";
import loginRequest from "../logic/api/proxy/user/loginRequest";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "../logic/hooks/useAuth";

function LoginPage() {
    let [login, setLogin] = useState("");
    let [password, setPassword] = useState("");
    let [error, setError] = useState(false);
    const { setAuth } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/";

    async function loginHandle() {
        await loginRequest(login, password, setError)
            .then((resp) => {
                window.localStorage.setItem("username", resp?.data?.userUsername);
                document.cookie = `accessToken=${resp?.data?.accessToken}; SameSite=Lax; Secure; max-age=300;`
                setAuth({
                    username: resp?.data?.userUsername,
                    accessToken: resp?.data?.accessToken
                });
                navigate(from, { replace: true });
            })
            .catch((err) => {
                console.log(err);
                setError(true);
            })
    }

    return (
        <div className="d-flex justify-content-center mt-5">
            <Wrapper width="800px" height="500px">
                <Container className="p-5">
                    <Row>
                        <Col xs={2}>
                            <label>Login:</label>
                        </Col>
                        <Col>
                            <input type="text" autoComplete="login" placeholder="Login" onChange={(e) => setLogin(e.target.value)} />
                        </Col>
                    </Row>
                    <Row>
                        <Col md={{offset: 8}}>
                            <NormalButton text="Sign in" action={loginHandle}/>
                        </Col>
                    </Row>
                    <Row>
                        <Col xs={2}>
                            <label>Password:</label>
                        </Col>
                        <Col>
                            <input type="password" placeholder="Password" onChange={(e) => setPassword(e.target.value)}/>
                        </Col>
                    </Row>
                    <Row className="mt-3 pt-3">
                        { error ? 
                        <div className="alert alert-danger">
                            Incorrect login or password.
                        </div> : 
                        "" }
                    </Row>
                    <Row className="mt-5 pt-5">
                        <Col>
                            <p>You have no account?</p>
                            <p>Just create one!</p>
                        </Col>
                        <Col md={{offset: 4}}>
                            <LoudButton text="Sign up" />
                        </Col>
                    </Row>
                </Container>
            </Wrapper>
        </div>
    )
}

export default LoginPage;