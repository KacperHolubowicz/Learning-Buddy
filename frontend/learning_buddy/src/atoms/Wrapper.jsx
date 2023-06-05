import Stack from "react-bootstrap/esm/Stack";

function Wrapper(props) {
    const styling = {
        width: props.width,
        height: props.height,
        borderStyle: "solid",
        borderWidth: "3px",
        borderColor: "#275B71",
        backgroundColor: "#DAEDFF",
        marginBottom: "20px"
    }

    return (<div style={styling}>
            {props.children}
    </div>);
}

export default Wrapper;