import { useState } from "react";

function NavbarItem({text, action}) {
    let [btnColor, setBtnColor] = useState("#DAEDFF");

    const styling = {
        maxWidth: "300px",
        minWidth: "200px",
        maxHeight: "80px",
        minHeight: "60px",
        borderLeftStyle: "solid",
        borderRightStyle: "solid",
        borderColor: btnColor,
        backgroundColor: "inherit",
        color: btnColor,
        fontSize: "24px",
        justifyContent: "center",
        display: "inline-flex",
        alignItems: "center",
        transition: "0.5s",
        cursor: "pointer"
    }

    return (
        <div style={styling} 
        onClick={action} 
        onMouseEnter={() => setBtnColor("#FB99FF")}
        onMouseLeave={() => setBtnColor("#DAEDFF")}
        >
            {text}
        </div>
    )
}

export default NavbarItem;