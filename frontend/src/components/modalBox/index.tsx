import { Box, IconButton, Modal, Tooltip, Typography } from "@mui/material";
import { useContext } from "react";
import { ModalBoxContext } from "./providers/modalBox";
import CloseIcon from "@mui/icons-material/Close";

const modalStyle = {
  position: "absolute" as "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 800,
  backgroundColor: "#FFFFFF",
  boxShadow: 24,
  p: 2,
};

const ModalBox: React.FC = () => {
  const { open, body, actionModalBox, title} = useContext(ModalBoxContext);
  const handleClose = () => actionModalBox(false, null, "");

  return (
    <Modal
      keepMounted
      open={open}
      onClose={(_, reason) => {
        if (reason !== "backdropClick") {
          handleClose();
        }
      }}
    >
      <Box sx={{ ...modalStyle }}>
        <Box sx={{ position: "relative" }}>
          <Box sx={{ position: "absolute", top: -8, right: -8 }}>
            <IconButton
              onClick={() => {
                 handleClose();
              }}
            >
              <Tooltip title="Zamkij okno" arrow>
                <CloseIcon />
              </Tooltip>
            </IconButton>
          </Box>
          <Typography id="keep-mounted-modal-title" component="h2" variant="h6" sx={{ mb: 2, fontWeight: 600, fontSize: 18 }}>
            {title}
          </Typography>
          {body}
        </Box>
      </Box>
    </Modal>
  );
};

export default ModalBox;
