function ListElementBackground(props) {
    return (
        <div style={styling}>
            {props.children}
        </div>
    )
}

const styling={
    maxWidth: "900px",
    maxHeight: "80px",
    backgroundColor: "#8BB4CD"
}

export default ListElementBackground;