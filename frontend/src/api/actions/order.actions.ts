import API from "..";
import {OrderRequest} from "../types";
import {ProductType} from "../../common/types";


export const addOrder = async (order: OrderRequest) =>{
    return await API.post("/AddOrder", order);
}

interface Product {
    name: string;
    price: number;
    section: string;
    categoryId: number;
    category: {
        id: number;
        name: string;
    };
}

interface Order {
    products: Product[];
    orderDate: string;
    id: number;
}
export const getTodaysOrders = async () : Promise<Order[]> => {
    const response = await API.get("/today-orders")
    const orders = response.data.map((order: any) => ({...order, orders: []}));
    return orders
}