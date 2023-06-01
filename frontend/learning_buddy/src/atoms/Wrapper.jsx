function Wrapper(props) {
    const styling = {
        width: props.width,
        height: props.height,
        borderStyle: "solid",
        borderWidth: "3px",
        borderColor: "#275B71",
        backgroundColor: "#DAEDFF"
    }

    return (<div style={styling}>
        {props.children}
    </div>);
}

export default Wrapper;