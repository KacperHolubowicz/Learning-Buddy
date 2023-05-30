function NormalButton({text, action}) {
    return (
    <button style={styling} onClick={action}>
        {text}
    </button>
    )
}

const styling = {
    maxWidth: "200px",
    maxHeight: "100px",
    backgroundColor: "#8BB4CD",
    color: "#E4F6FF",
    border: "3px solid #8BB4CD",
    borderRadius: "30px",
    fontSize: "24px",
    fontWeight: "bold"
}

export default NormalButton;