function NormalButton({text, action, disable}) {
    return (
    <button style={styling} onClick={action} disabled={disable}>
        {text}
    </button>
    )
}

const styling = {
    minWidth: "150px",
    maxWidth: "200px",
    minHeight: "40px",
    maxHeight: "100px",
    backgroundColor: "#8BB4CD",
    color: "#E4F6FF",
    border: "3px solid #8BB4CD",
    borderRadius: "30px",
    fontSize: "24px",
    fontWeight: "bold"
}

export default NormalButton;