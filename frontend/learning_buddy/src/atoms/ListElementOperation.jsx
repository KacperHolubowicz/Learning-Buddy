import { useState } from "react";

function ListElementOperation({text, action}) {
    let [btnColor, setBtnColor] = useState("#C31EEA");
    const styling = {
        minWidth: "80px",
        maxWidth: "150px",
        maxHeight: "200px",
        borderLeftStyle: "solid",
        borderRightStyle: "solid",
        borderColor: "#DAEDFF",
        backgroundColor: "inherit",
        color: btnColor,
        fontSize: "24px",
        justifyContent: "center",
        display: "inline-flex",
        alignItems: "center",
        textAlign: "center",
        transition: "0.5s",
        cursor: "pointer",
        borderWidth: "thin"
    }

    return (
        <div style={styling} onClick={action}
        onMouseEnter={() => setBtnColor("#FB99FF")}
        onMouseLeave={() => setBtnColor("#C31EEA")}
        >
            {text}
        </div>
    )
}

export default ListElementOperation;