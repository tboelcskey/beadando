import React from "react";
import Button from "@material-ui/core/Button";
import SaveIcon from "@material-ui/icons/Save";
import axios from "axios";

function UploadImage(props) {
  props.setIsUploading(true);
  axios
    .post("https://localhost:44380/api/image", {
      base64EncodedImage: props.image.split(",")[1],
      imageNameWithExtension: Math.random().toString(36).substring(7) + ".jpg",
    })
    .then((res) => {
      props.setSelected(false);
      props.setSuccessful(true);
      props.setIsUploading(false);
      props.setOpen(true);
    })
    .catch(()=>{
      props.setSuccessful(false)
      props.setIsUploading(false);
      props.setOpen(true);
    });    
}

export default function UploadImageButton(props) {
  return (
    <div>
      <Button
        onClick={() => UploadImage(props)}
        variant="contained"
        color="secondary"
        startIcon={<SaveIcon />}
      >
        Feltöltés
      </Button>
    </div>
  );
}
