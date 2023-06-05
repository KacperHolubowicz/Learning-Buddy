function ListElementImage({image, width, height, alt}) {
  return (
        <img src={`data:image/jpeg;base64,${image}`} height={height} width={width} alt={alt}/>
    )
}

export default ListElementImage;