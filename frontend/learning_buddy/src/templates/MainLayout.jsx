import { Outlet } from "react-router-dom";
import Navbar from "../organisms/Navbar";

function MainLayout() {
    return (
        <>
            <Navbar />
            <Outlet />
        </>
    )
}

export default MainLayout;