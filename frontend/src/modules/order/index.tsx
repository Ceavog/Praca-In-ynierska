import { Box, Button, Icon, Stack, Tab, Tabs, Typography } from "@mui/material";
import { getCategories, getProducts } from "api/actions/settings.actions";
import { useReducer, useState } from "react";
import { useMutation, useQuery } from "react-query";
import ArrowRightIcon from "@mui/icons-material/ArrowRight";
import ProductRow from "./productRow";
import TabPanel from "./tabPanel";
import { groupBy } from "lodash";
import { ProductType } from "../../common/types";
import Row from "./StyledRow";
import DeleteIcon from "@mui/icons-material/Delete";
import { OrderRequest } from "../../api/types";
import { addOrder } from "api/actions/order.actions";

const Order = () => {
  const [reducer, forceUpdate] = useReducer((x) => x + 1, 0);
  const [value, setValue] = useState(0);
  const { data: categories} = useQuery(["categories"], getCategories);
  const { data: productsData } = useQuery(["products"], getProducts);
  const products = groupBy(productsData, (item) => item.categoryId);
  const categoriesGrouped = categories?.map((item) => {
    if (products[item.id]) {
      return { ...item, positions: products[item.id] };
    } else {
      return item;
    }
  });

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  const [cart, setCart] = useState<ProductType[]>([]);
  const addToCart = (newItem: ProductType) => {
    setCart([...cart, newItem]);
  };
  const removeFromCart = (index: number) => {
    cart.splice(index, 1);
    setCart(cart);
    forceUpdate();
  };

  const { mutate } = useMutation(addOrder, {
    onSuccess: () => {
      window.location.reload();
    },
    onError: (err: any) => {
      alert(`Nie udało się - ${err.response.data}`);
    },
  });

  const onSubmit = async (data: ProductType[]) => {
    const order: OrderRequest = new OrderRequest();
    order.productIds = data.map(({ id }) => id);

    mutate(order);
  };

  return (
    <>
      <Box sx={{ mt: 4, display: "flex", justifyContent: "center" }}>
        {cart.length > 0 && (
          <Box>
            {cart.map((item, index) => (
              <Row key={item.id} sx={{ display: "flex", columnGap: 2 }}>
                <Typography gutterBottom color="grey.900" fontWeight="500" component="div">
                  {item.name}
                </Typography>
                <Typography gutterBottom color="grey.900" component="div">
                  {item.price} zł
                </Typography>
                <Typography>
                  <Icon
                    onClick={() => {
                      removeFromCart(index);
                    }}
                  >
                    <DeleteIcon />
                  </Icon>
                </Typography>
              </Row>
            ))}
            <Button
              sx={{ mt: 6 }}
              variant="outlined"
              onClick={() => {
                onSubmit(cart);
              }}
            >
              DODAJ ZAMÓWIENIE
            </Button>
          </Box>
        )}
      </Box>
      <Box
        sx={{
          maxWidth: 1280,
          mx: "auto",
          my: 8,
          borderRadius: "8px",
          boxShadow: "rgba(0, 0, 0, 0.05) 0px 6px 24px 0px, rgba(0, 0, 0, 0.08) 0px 0px 0px 1px",
        }}
      >
        <Box sx={{ flexGrow: 1, display: "flex" }}>
          <Tabs
            orientation="vertical"
            variant="scrollable"
            value={value}
            onChange={handleChange}
            sx={{ borderRight: 1, borderColor: "divider", flex: "240px 0" }}
          >
            {categoriesGrouped?.map((category, index) => (
              <Tab
                key={index}
                sx={{ textTransform: "none", justifyContent: "space-between" }}
                icon={<ArrowRightIcon color="action" />}
                iconPosition="end"
                label={category.name}
              />
            ))}
          </Tabs>
          {categoriesGrouped?.map((category, index) => (
            <TabPanel value={value} index={index} key={index}>
              <Box width="100%">
                <Typography variant="h6" component="p" sx={{ mb: 5 }}>
                  {category.name}
                </Typography>
                <Stack spacing={2}>
                  {category.positions.map((product) => (
                    <ProductRow product={product} key={product.id} addToCart={addToCart} />
                  ))}
                </Stack>
              </Box>
            </TabPanel>
          ))}
        </Box>
      </Box>
    </>
  );
};

export default Order;
