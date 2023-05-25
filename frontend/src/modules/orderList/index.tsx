import React, { useState } from 'react';
import { Box, Container, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography, Collapse } from '@mui/material';
import { useQuery } from "react-query";
import { getTodaysOrders } from "../../api/actions/order.actions";
import OrderRow from './orderRow';

function OrderList() {
    const { data: orderData } = useQuery(["orders"], getTodaysOrders);
    const filteredData = orderData?.filter(order => order.products.length > 0);
    const [expanded, setExpanded] = useState<number | null>(null);
    const handleExpandClick = (orderId: number) => {
        setExpanded(prevExpanded => (prevExpanded === orderId ? null : orderId));
    };

    return (
        <Container maxWidth="lg">
            <Box sx={{ my: 4 }}>
                <Typography variant="h4" component="h2" gutterBottom>
                    Zamówienia
                </Typography>
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Numer</TableCell>
                                <TableCell>Godzina zamówienia</TableCell>
                                <TableCell>Łączna cena</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {filteredData?.map((order, orderIndex) => (
                                <OrderRow
                                    key={order.id}
                                    order={order}
                                    orderIndex={orderIndex}
                                    expanded={expanded === order.id}
                                    handleExpandClick={handleExpandClick}
                                />
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>
        </Container>
    );
}

export default OrderList;
