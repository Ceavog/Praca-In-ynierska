import {Box, styled} from "@mui/material";

const Row = styled(Box)(() => ({
    cursor: "pointer",
    "&:hover": {
        boxShadow: "rgba(0, 0, 0, 0.05) 0px 6px 24px 0px, rgba(0, 0, 0, 0.08) 0px 0px 0px 1px",
        "& .addIcon": {
            transition: "background-color 200ms linear",
            background: " #E5E7EB",
        },
    },
    borderRadius: "8px",
    position: "relative",
    padding: "10px"
}));

export default Row;
