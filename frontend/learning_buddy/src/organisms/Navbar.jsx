import NavbarBackground from "../atoms/NavbarBackground";
import NavbarItem from "../atoms/NavbarItem";
import { useNavigate } from "react-router-dom";
import {Container, Row, Col} from "react-bootstrap";

function Navbar() {
    const navigate = useNavigate();

    return (
        <NavbarBackground>
            <Container fluid>
                <Row>
                    <Col>
                        <NavbarItem text="All subjects" action={() => navigate("/subjects")}/>
                    </Col>
                    <Col style={{display:'flex', justifyContent:'right'}}>
                        <NavbarItem text="Sign in" action={() => navigate("/login")} />
                    </Col>
                </Row>
            </Container>
        </NavbarBackground>
    )
}

export default Navbar;