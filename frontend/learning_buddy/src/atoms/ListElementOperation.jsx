import { useState } from "react";

function ListElementOperation({text, action}) {
    let [btnColor, setBtnColor] = useState("#C31EEA");
    const styling = {
        minWidth: "180px",
        maxWidth: "240px",
        maxHeight: "100px",
        borderLeftStyle: "solid",
        borderRightStyle: "solid",
        borderColor: btnColor,
        backgroundColor: "inherit",
        color: btnColor,
        fontSize: "24px",
        justifyContent: "center",
        display: "inline-flex",
        alignItems: "center",
        textAlign: "center",
        transition: "0.5s",
        cursor: "pointer"
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