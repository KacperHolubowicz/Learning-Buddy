import { useState } from "react";

function ListElementOperation({text, action}) {
    let [btnColor, setBtnColor] = useState("#C31EEA");
    const styling = {
        minWidth: "50px",
        maxWidth: "150px",
        minHeight: "80px",
        maxHeight: "200px",
        borderLeftStyle: "solid",
        borderRightStyle: "solid",
        borderColor: "#DAEDFF",
        backgroundColor: "inherit",
        color: btnColor,
        fontSize: "18px",
        justifyContent: "center",
        display: "inline-flex",
        alignItems: "center",
        textAlign: "center",
        transition: "0.5s",
        cursor: "pointer",
        borderWidth: "thin",
        paddingLeft: "5px",
        paddingRight: "5px"
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