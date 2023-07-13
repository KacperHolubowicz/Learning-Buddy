import { createContext, useState } from "react";

const AuthContext = createContext({});

export const AuthProvider = ({ children }) => {
    const username = getUsername();
    const [auth, setAuth] = useState({
        username: username
    });

    return (
        <AuthContext.Provider value={{ auth, setAuth }}>
            {children}
        </AuthContext.Provider>
    )
}

function getUsername() {
    const token = document.cookie
        .split("; ")
        .find((row) => row.startsWith("username="))
        ?.split("=")[1];
    
    if(token === undefined) {
        return null;
    }
    console.log("Logged as " + token);
    return token;
}

export default AuthContext;