import defaultThumbnail from "../resources/no-thumbnail.png";

function ListElementImage({image, width, height, alt}) {
  const thumbnailSource = image === null || image === undefined || image.length === 0 ? defaultThumbnail : `data:image/jpeg;base64,${image}`;
  return (
        <img src={thumbnailSource} height={height} width={width} alt={alt}/>
    )
}

export default ListElementImage;