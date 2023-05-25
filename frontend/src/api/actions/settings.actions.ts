import API from "..";
import {OrderRequest} from "../types";
import {CategoryType, ProductType} from "../../common/types";


export const getProducts = async (): Promise<ProductType[]> => {
  const response = await API.get("/GetAllProductsByUserId");
  return response.data.value;
};


type AddProductVars = {
  name: string;
  price: number;
  section: number;
  userId: number;
  categoryId: number;
};

export const addProduct = async (addProductVars: AddProductVars) => {
  return await API.post("/AddProduct", addProductVars);
};

export const deleteProduct = async (id: number) => {
  return await API.delete(`/DeleteProductById?id=${id}`);
};

type UpdateProductVars = {
  id: number;
  name?: string;
  price?: number;
  section?: number;
  userId?: number;
  categoryId?: number;
};

export const updateProduct = async (updateProductVars: UpdateProductVars) => {
  return await API.put("/UpdateProduct", updateProductVars);
};

type AddCategoryVars = {
  name: string;
  userId: number;
};

export const getCategories = async (): Promise<CategoryType[]> => {
  console.log("api");
  const response = await API.get(`/GetAllCategoriesForUserId`);
  const categories = response.data.map((cat: any) => ({ ...cat, positions: [] }));

  return categories;
};

export const addCategory = async (addCategoryVars: AddCategoryVars) => {
  return await API.post("/AddCategory", addCategoryVars);
};

export const deleteCategory = async (id: number) => {
  return await API.delete(`/DeleteCategoryById?id=${id}`);
};

