import { GetMyOrder } from '@/app/api/order/orderActions'
import React from 'react'
import OrderForm from '../OrderForm/OrderForm';

export default async function page() {

    const response = await GetMyOrder();


    const totalItemPrice = () => {
        var totalPrice = 0;
        for (let index = 0; index < response.length; index++) {
            const element = response[index];
            totalPrice = totalPrice + element.price
        }
        return totalPrice
    }

    var totalPrice = totalItemPrice();

  return (
  <OrderForm totalPrice={totalPrice} orders={response} ></OrderForm>
  )
}
