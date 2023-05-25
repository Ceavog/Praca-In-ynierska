export interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}


export type ProductType = {
  categoryId: number;
  id: number;
  name: string;
  price: number;
};


export type CategoryType = {
  categoryId: number;
  id: number;
  name: string;
  positions: ProductType[];
};