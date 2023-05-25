import { useContext, useEffect, useState } from "react";
import {Box, Button, Grid, Stack, Typography} from "@mui/material";
import AddCategoryForm from "./components/forms/addCategoryForm";
import { ModalBoxContext } from "components/modalBox/providers/modalBox";
import AddProductForm from "./components/forms/addProductForm";
import { useQuery } from "react-query";
import { getCategories, getProducts } from "api/actions/settings.actions";
import { groupBy } from "lodash";
import Item from "./components/item";
import {CategoryType} from "../../../common/types";

const SettingsProducts = () => {
  const { actionModalBox } = useContext(ModalBoxContext);


  const [items, setItems] = useState<CategoryType[]>([]);

  const { data: categories, isLoading: loadingCategories, refetch: fetchCategories} = useQuery(["categories"], getCategories);
  const { data: productsData, isLoading: loadingProducts, refetch: fetchProducts} = useQuery(["products"], getProducts);

  const refreshData = async () => {
    await fetchCategories();
    await fetchProducts();
  };


  const products = groupBy(productsData, (item) => item.categoryId);
  const categoriesGrouped = categories?.map((item) => {
    if (products[item.id]) {
      return { ...item, positions: products[item.id] };
    } else {
      return item;
    }
  });



  useEffect(() => {
    if (!(loadingCategories && loadingProducts) && categoriesGrouped) {
      setItems(categoriesGrouped);
    }
  }, [categories, productsData]);

  let content;
  if (loadingCategories && loadingProducts) {
    content = <></>;
  } else {
    content = (
      <Box sx={{ "& li": { listStyleType: "none" } }}>
            <Stack spacing={2}>
              {items.map((item) => (
                <Item id={item.id} item={item} key={item.id} onMenuEdit={refreshData}/>
              ))}
            </Stack>
      </Box>
    );
  }

  return (
    <Box sx={{ maxWidth: 1048, mx: "auto", mt: 8 }}>
      <Grid container justifyContent="space-between" alignItems="center" sx={{ mb: 4 }}>
        <Typography variant="h4" fontWeight="600" fontSize={24}>
          Kategorie i produkty
        </Typography>
        <Box>
          <Button
            sx={{ mr: 1 }}
            variant="contained"
            onClick={() => {
              actionModalBox(true, <AddProductForm onMenuEdit={refreshData}/>, "Nowy produkt");
            }}
          >
            Dodaj produkt
          </Button>
          <Button
            variant="contained"
            onClick={() => {
              actionModalBox(true, <AddCategoryForm onMenuEdit={refreshData} />, "Nowa kategoria");
            }}
          >
            Dodaj kategorie
          </Button>
        </Box>
      </Grid>
      {content}
    </Box>
  );
};

export default SettingsProducts;
