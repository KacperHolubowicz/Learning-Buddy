function ListElementItem({text}) {

    const styling = {
        minWidth: "50px",
        maxWidth: "300px",
        minHeight: "80px",
        maxHeight: "200px",
        borderLeftStyle: "solid",
        borderRightStyle: "solid",
        borderColor: "#DAEDFF",
        backgroundColor: "inherit",
        color: "#DAEDFF",
        fontSize: "18px",
        justifyContent: "center",
        borderWidth: "thin",
        display: "inline-flex",
        alignItems: "center",
        textAlign: "center",
        paddingLeft: "5px",
        paddingRight: "5px"
    }

    return (
        <div style={styling}>
            {text}
        </div>
    )
}

export default ListElementItem;