import NavbarBackground from "../atoms/NavbarBackground";
import NavbarItem from "../atoms/NavbarItem";
import { useNavigate } from "react-router-dom";
import {Container, Row, Col} from "react-bootstrap";
import useAuth from "../logic/hooks/useAuth";

function Navbar() {
    const navigate = useNavigate();
    const { auth } = useAuth();

    return (
        <NavbarBackground>
            <Container fluid>
                <Row xs>
                    <Col xs={2}>
                        <NavbarItem text="All subjects" action={() => navigate("/subjects")}/>
                    </Col>
                    { auth?.username !== null ?
                    <>
                        <Col xs={2}>
                            <NavbarItem text="Your subjects" action={() => navigate("/private-subjects")}/>
                        </Col>
                        <Col xs={2}>
                            <NavbarItem text="Favourites" action={() => navigate("/favourites")}/>
                        </Col>
                        <Col xs={2}>
                            <NavbarItem text="Your account" action={() => navigate("/user")}/>
                        </Col>
                        <Col style={{display:'flex', justifyContent:'right'}}>
                            <NavbarItem text="Logout" action={() => navigate("/logout")} />
                        </Col>
                    </> :
                    <Col style={{display:'flex', justifyContent:'right'}}>
                        <NavbarItem text="Sign in" action={() => navigate("/login")} />
                    </Col>
                    }
                </Row>
            </Container>
        </NavbarBackground>
    )
}

export default Navbar;