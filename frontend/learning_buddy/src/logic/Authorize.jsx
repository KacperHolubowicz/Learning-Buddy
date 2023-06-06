import { useContext } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import AuthContext from "./auth";

function Authorize() {
    const { auth } = useContext(AuthContext);
    const location = useLocation();
    
    return auth?.username ? <Outlet /> : <Navigate to="/login" state={{from: location}} replace/>
}

export default Authorize;