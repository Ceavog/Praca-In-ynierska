import React from 'react';
import { TableRow, TableCell, Collapse, Box, Table, TableHead, TableBody } from '@mui/material';

interface Product {
    name: string;
    price: number;
    category: {
        name: string;
    };
}

interface Order {
    id: number;
    orderDate: string;
    products: Product[];
}

interface OrderRowProps {
    order: Order;
    orderIndex: number;
    expanded: boolean;
    handleExpandClick: (orderId: number) => void;
}

const isOrderOlderThanOneHour = (orderDate: string): boolean => {
    const oneHourAgo = new Date(Date.now() - 60 * 60 * 1000);
    const orderDateTime = new Date(orderDate);
    return orderDateTime < oneHourAgo;
};

const OrderRow: React.FC<OrderRowProps> = ({ order, orderIndex, expanded, handleExpandClick }) => {
    const totalPrice = order.products.reduce((sum: number, product: Product) => sum + product.price, 0);
    const orderHour = new Date(order.orderDate).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

    const olderThanOneHour = isOrderOlderThanOneHour(order.orderDate);

    return (
        <>
            <TableRow
                key={order.id}
                onClick={() => handleExpandClick(order.id)}
                sx={{
                    cursor: 'pointer',
                    backgroundColor: olderThanOneHour ? 'rgba(237, 5, 16, 0.5)' : 'inherit',
                    '&:last-child td, &:last-child th': { border: 0 },
                    '&:nth-of-type(even)': { backgroundColor: 'rgba(0, 0, 0, 0.03)' },
                }}
            >
                <TableCell sx={{ py: 1.5 }}>{orderIndex + 1}</TableCell>
                <TableCell sx={{ py: 1.5 }}>{orderHour}</TableCell>
                <TableCell sx={{ py: 1.5 }}>{totalPrice} zł </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={3}>
                    <Collapse in={expanded} timeout="auto" >
                        <Box margin={1}>
                            <Table size="small">
                                <TableHead>
                                    <TableRow>
                                        <TableCell sx={{ fontWeight: 'bold' }}>Nazwa produktu</TableCell>
                                        <TableCell sx={{ fontWeight: 'bold' }}>Cena produktu</TableCell>
                                        <TableCell sx={{ fontWeight: 'bold' }}>Nazwa kategorii</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {order.products.map((product: Product, index: number) => (
                                        <TableRow key={`${order.id}_${index}`} sx={{ '&:nth-of-type(even)': { backgroundColor: 'rgba(0, 0, 0, 0.03)' } }}>
                                            <TableCell sx={{ py: 0.5 }}>{product.name}</TableCell>
                                            <TableCell sx={{ py: 0.5 }}>{product.price} zł</TableCell>
                                            <TableCell sx={{ py: 0.5 }}>{product.category.name}</TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </>
    );
};

export default OrderRow;
