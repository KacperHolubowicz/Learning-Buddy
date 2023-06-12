function QuietButton({text, action}) {
    return (
    <button style={styling} onClick={action}>
        {text}
    </button>
    )
}

const styling = {
    width: "200px",
    minHeight: "80px",
    maxHeight: "100px",
    backgroundColor: "inherit",
    color: "#8BB4CD",
    border: "3px solid #8BB4CD",
    borderRadius: "30px",
    fontSize: "24px",
    fontWeight: "bold"
}

export default QuietButton;