function LoudButton({text, action, disable, submit=false}) {
    return (
    <button style={styling} onClick={action} disabled={disable} type={submit ? "submit" : ""}>
        {text}
    </button>
    )
}

const styling = {
    minWidth: "150px",
    maxWidth: "200px",
    minHeight: "40px",
    maxHeight: "100px",
    backgroundColor: "#C31EEA",
    color: "#E4F6FF",
    border: "3px solid #C31EEA",
    borderRadius: "30px",
    fontSize: "24px",
    fontWeight: "bold"
}

export default LoudButton;