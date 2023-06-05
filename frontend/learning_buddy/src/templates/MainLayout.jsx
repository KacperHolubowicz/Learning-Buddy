import { Outlet } from "react-router-dom";
import Navbar from "../organisms/Navbar";
import AuthContext from "../logic/auth";

function MainLayout() {

    return (
        <AuthContext.Provider>
            <Navbar />
            <Outlet />
        </AuthContext.Provider>
    )
}

export default MainLayout;