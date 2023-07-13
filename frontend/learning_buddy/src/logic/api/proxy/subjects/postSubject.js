import axiosApi from "../../axios";

export default async function postSubject(name, description = "", isPublic, tags, thumbnail) {
    console.log("THUMBNAIL: " + thumbnail);
    return await axiosApi.post('subject', JSON.stringify({
        name: name,
        description: description,
        public: isPublic,
        tags: tags,
        thumbnail: splitThumbnail(thumbnail)
    }));
}

function splitThumbnail(thumbnail) {
    return thumbnail.split(",")[1];
}