function ListElementLongItem({text}) {

    const styling = {
        minWidth: "300px",
        maxWidth: "600px",
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
        paddingRight: "5px",
        wordWrap: "break-word"
    }

    return (
        <div style={styling}>
            {text}
        </div>
    )
}

export default ListElementLongItem;