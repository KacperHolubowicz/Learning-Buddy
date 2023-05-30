function NavbarBackground(props) {
    return (
        <div style={styling}>
            {props.children}
        </div>
    )
}

const styling = {
    width: "100%",
    backgroundColor: "#275B71"
}

export default NavbarBackground;