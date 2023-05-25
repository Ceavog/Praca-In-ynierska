import AddIcon from "@mui/icons-material/Add";
import { Box, Icon, styled, Typography } from "@mui/material";
import { ProductType } from "../../../common/types";
import Row from "../StyledRow";

type ProductRowProps = {
  product: ProductType;
  addToCart: (newItem: ProductType) => void;
};

const ProductRow: React.FC<ProductRowProps> = ({ product, addToCart }) => {
  return (
    <Row
      onClick={() => {
        addToCart(product);
      }}
    >
      <Box sx={{ mr: 4 }}>
        <Typography gutterBottom color="grey.900" fontWeight="500" component="div">
          {`${product.name}`}
        </Typography>
        <Typography color="grey.900" component="div">
          {`${product.price} zł`}
        </Typography>
      </Box>
      <Icon
        className="addIcon"
        sx={{
          position: "absolute",
          right: 0,
          top: 0,
          borderRadius: "0 10px 0 10px",
          width: 32,
          height: 32,
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <AddIcon />
      </Icon>
    </Row>
  );
};

export default ProductRow;
