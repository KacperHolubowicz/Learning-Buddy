function ListElementItem({text}) {

    const styling = {
        minWidth: "140px",
        maxWidth: "300px",
        height: "100px",
        borderLeftStyle: "solid",
        borderRightStyle: "solid",
        borderColor: "#DAEDFF",
        backgroundColor: "inherit",
        color: "#DAEDFF",
        fontSize: "24px",
        justifyContent: "center",
        display: "inline-flex",
        alignItems: "center",
        textAlign: "center"
    }

    return (
        <div style={styling}>
            {text}
        </div>
    )
}

export default ListElementItem;