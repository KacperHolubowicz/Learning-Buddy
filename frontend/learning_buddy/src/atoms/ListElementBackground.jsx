function ListElementBackground(props) {
    return (
        <div style={styling}>
            {props.children}
        </div>
    )
}

const styling={
    width: "1200px",
    height: "100px",
    backgroundColor: "#8BB4CD",
}

export default ListElementBackground;