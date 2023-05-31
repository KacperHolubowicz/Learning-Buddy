function LoudButton({text, action}) {
    return (
    <button style={styling} onClick={action}>
        {text}
    </button>
    )
}

const styling = {
    maxWidth: "200px",
    maxHeight: "100px",
    backgroundColor: "#C31EEA",
    color: "#E4F6FF",
    border: "3px solid #C31EEA",
    borderRadius: "30px",
    fontSize: "24px",
    fontWeight: "bold"
}

export default LoudButton;