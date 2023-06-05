function ListElementItem({text}) {

    const styling = {
        minWidth: "140px",
        maxWidth: "300px",
        height: "200px",
        borderLeftStyle: "solid",
        borderRightStyle: "solid",
        borderColor: "#DAEDFF",
        backgroundColor: "inherit",
        color: "#DAEDFF",
        fontSize: "24px",
        justifyContent: "center",
        display: "inline-flex",
        alignItems: "center",
        textAlign: "center",
        marginLeft: "5px",
        marginRight: "5px"
    }

    return (
        <div style={styling}>
            {text}
        </div>
    )
}

export default ListElementItem;