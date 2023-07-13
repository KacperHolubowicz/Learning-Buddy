import { Container, Row, Col } from "react-bootstrap";
import Wrapper from "../../atoms/Wrapper";
import { useState } from "react";
import LoudButton from "../../atoms/LoudButton";
import postSubject from "../../logic/api/proxy/subjects/postSubject";
import { useNavigate } from "react-router-dom";

function SubjectCreatePage() {
    let [name, setName] = useState("");
    let [description, setDescription] = useState("");
    let [publicSubject, setPublicSubject] = useState(false);
    let [tagString, setTagString] = useState("");
    let [tags, setTags] = useState([]);
    let [thumbnail, setThumbnail] = useState("");
    let [nameError, setNameError] = useState("");
    let [tagStringError, setTagStringError] = useState("");
    let [thumbnailError, setThumbnailError] = useState("");
    let [descriptionError, setDescriptionError] = useState("");
    const navigate = useNavigate();
    let newSubjectId;

    async function createSubject(e) {
        e.preventDefault();
        if(nameValidation()) {
            await postSubject(name, description, publicSubject, tags, thumbnail)
                .then((resp) => {
                    newSubjectId = resp.data;
                })
                .catch((err) => {
                    console.log(err);
                });
            navigate(`/subjects/${newSubjectId}`);
        }
    }

    const toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = reject;
    });

    function nameValidation() {
        if(name.length <= 0) {
            setNameError("Subject name must be provided");
            return false;
        }
        return true;
    }

    function descriptionValidation(desc) {
        if(description.length > 200) {
            setDescriptionError("Description cannot be longer than 200 characters");
            return;
        }
        setDescription(desc);
    }

    function tagStringValidation(tagsVal) {
        const format = /[!@#$%^&*()_+\-=\[\]{};':"\\|.<>\/?]+/;
        if(format.test(tagsVal)) {
            setTagStringError("Tags cannot contain any special characters other than comma");
            return;
        }

        const tagsArray = tagsVal.split(",");
        if(tagsArray.length > 3) {
            setTagStringError("Do not provide more than 3 tags");
            return;
        }

        setTagStringError("");
        setTagString(tagsVal);
        setTags(tagsArray);
    }

    function validateThumbnail(event) {
        var re = /(\.jpg|\.jpeg|\.gif|\.png)$/i;
        const file = event.target.files[0];

        if (!re.exec(file.name)) {
            setThumbnailError("File extension not supported!");
        } else if(file.size > 8388608) {
            setThumbnailError("This file is too big");
        } else {
            console.log(file);
            toBase64(file)
                .then((resp) => {
                    console.log(resp);
                    setThumbnail(resp);
                    setThumbnailError("");
                })
                .catch((err) => {
                    console.log(err);
                });
        }
    }

    function checkError() {
        return tagStringError !== "" || thumbnailError !== "";
    }

    return (
        <Container className="mt-3">
            <Wrapper>
                <form>
                    {
                        nameError !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {nameError}
                            </div>
                        </Row> :
                        ""
                    }
                    {
                        thumbnailError !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {thumbnailError}
                            </div>
                        </Row> :
                        ""
                    }
                    {
                        descriptionError !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {descriptionError}
                            </div>
                        </Row> :
                        ""
                    }
                    {
                        tagStringError !== "" ?
                        <Row className="mt-2 pe-3 px-3">
                            <div className="alert alert-danger">
                                {tagStringError}
                            </div>
                        </Row> :
                        ""
                    }
                    <Row className="mt-2 pe-3 px-3">
                        <Col>
                            <h3>Subject name</h3>
                        </Col>
                        <Col>
                            <input type="text" placeholder="Subject name" value={name} onChange={(e) => setName(e.target.value)} />
                        </Col>
                    </Row>
                    <Row className="mt-2 pe-3 px-3">
                        <Col>
                            <h3>Optional description</h3>
                        </Col>
                        <Col>
                            <textarea placeholder="Description" value={description} onChange={(e) => descriptionValidation(e.target.value)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Public</h3>
                        </Col>
                        <Col>
                            <input type="checkbox" value={publicSubject} onChange={() => setPublicSubject(!publicSubject)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Comma separated tags (max 3)</h3>
                        </Col>
                        <Col>
                            <input type="text" value={tagString} onChange={(e) => tagStringValidation(e.target.value)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3">
                        <Col>
                            <h3>Optional thumbnail (max 8 MB)</h3>
                        </Col>
                        <Col>
                            <input type="file" name="myImage" accept="image/png, image/gif, image/jpeg, image/jpg" onChange={(e) => validateThumbnail(e)} />
                        </Col>
                    </Row>
                    <Row className="mt-5 pe-3 px-3 pb-3">
                        <Col>
                            <LoudButton text="Create" disable={checkError()} action={(e) => createSubject(e)} />
                        </Col>
                    </Row>
                </form>
            </Wrapper>
        </Container>
    );
}

export default SubjectCreatePage;