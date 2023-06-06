import { Outlet } from "react-router-dom";
import Navbar from "../organisms/Navbar";
import AuthContext from "../logic/auth";

function MainLayout() {

    return (
        <>
            <Navbar />
            <Outlet />
        </>
    )
}

export default MainLayout;