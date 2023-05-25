import { alpha, Box, Divider, Grid, IconButton, Menu, MenuItem, MenuProps, Paper, styled, TextField } from "@mui/material";
import { forwardRef, HTMLAttributes } from "react";
import React from "react";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import EditIcon from "@mui/icons-material/Edit";
import FileCopyIcon from "@mui/icons-material/FileCopy";
import DragIndicatorIcon from "@mui/icons-material/DragIndicator";
import DeleteIcon from "@mui/icons-material/Delete";
import { UniqueIdentifier } from "@dnd-kit/core";
import { useMutation } from "react-query";
import { deleteProduct } from "api/actions/settings.actions";
import {ProductType} from "../../../../../common/types";

const PaperElement = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(1, 1, 1, 4),
}));

export interface Props extends Omit<HTMLAttributes<HTMLDivElement>, "id"> {
  // activeId: string | number;
  id: UniqueIdentifier;
  item: ProductType | null;
  handleProps?: any;
  wrapperRef?(node: HTMLDivElement): void;
  onMenuEdit: () => void;

}

const StyledMenu = styled((props: MenuProps) => (
  <Menu
    elevation={0}
    anchorOrigin={{
      vertical: "bottom",
      horizontal: "right",
    }}
    transformOrigin={{
      vertical: "top",
      horizontal: "right",
    }}
    {...props}
  />
))(({ theme }) => ({
  "& .MuiPaper-root": {
    borderRadius: 6,
    marginTop: theme.spacing(1),
    minWidth: 180,
    color: theme.palette.mode === "light" ? "rgb(55, 65, 81)" : theme.palette.grey[300],
    boxShadow:
      "rgb(255, 255, 255) 0px 0px 0px 0px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px, rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px",
    "& .MuiMenu-list": {
      padding: "4px 0",
    },
    "& .MuiMenuItem-root": {
      "& .MuiSvgIcon-root": {
        fontSize: 18,
        color: theme.palette.text.secondary,
        marginRight: theme.spacing(1.5),
      },
      "&:active": {
        backgroundColor: alpha(theme.palette.primary.main, theme.palette.action.selectedOpacity),
      },
    },
  },
}));

const ProductItem = forwardRef<HTMLDivElement, Props>(({ id, item, style, wrapperRef, handleProps, ...props }, ref) => {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const { onMenuEdit } = props;

  const { mutate } = useMutation(deleteProduct, {
    onSuccess: () => {
      onMenuEdit()
    },
    onError: (err: any) => {
      alert(err);
    },
  });

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <div ref={wrapperRef} {...props}>
      <div ref={ref} style={style}>
        <Grid container component={PaperElement}>
          <Grid item xs={2}>
            <IconButton {...handleProps} sx={{ px: 3 }} disableRipple>
              <DragIndicatorIcon />
            </IconButton>
          </Grid>
          <Grid item xs={9}>
            <Box sx={{ mx: 3 }}>
              <TextField id="name" defaultValue={item?.name || ""} size="small" sx={{ mr: 1 }} disabled />
              <TextField id="price" defaultValue={"12"} size="small" disabled />
            </Box>
          </Grid>
          <Grid item xs={1}>
            <IconButton onClick={handleClick}>
              <MoreVertIcon />
            </IconButton>
            <StyledMenu
              id="demo-customized-menu"
              MenuListProps={{
                "aria-labelledby": "demo-customized-button",
              }}
              anchorEl={anchorEl}
              open={open}
              onClose={handleClose}
            >
              <MenuItem onClick={handleClose} disableRipple>
                <EditIcon />
                Edit
              </MenuItem>
              <MenuItem onClick={handleClose} disableRipple>
                <FileCopyIcon />
                Duplicate
              </MenuItem>
              <Divider sx={{ my: 0.5 }} />
              <MenuItem
                onClick={() => {
                  item && mutate(item.id);
                  handleClose();
                }}
                disableRipple
              >
                <DeleteIcon />
                Remove
              </MenuItem>
            </StyledMenu>
          </Grid>
        </Grid>
      </div>
    </div>
  );
});

export default ProductItem;
