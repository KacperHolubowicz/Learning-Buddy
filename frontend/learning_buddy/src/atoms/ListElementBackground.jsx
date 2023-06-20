function ListElementBackground(props) {
    return (
        <div style={styling}>
            {props.children}
        </div>
    )
}

const styling={
    width: "1400px",
    minHeight: "80px",
    maxHeight: "200px",
    backgroundColor: "#8BB4CD",
    display: "inline-flex"
}

export default ListElementBackground;