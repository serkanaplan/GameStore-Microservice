'use server'
import { getBasketItems } from '@/app/api/basket/basketActions';
import { Button } from 'flowbite-react';
import React from 'react'
import CheckoutButton from '../CheckoutPayment/CheckoutButton';

export default async function Cart() {
    const basketItems = await getBasketItems();



    const totalPrice = () => {
        var total=0
        for (let index = 0; index < basketItems.data.length; index++) {
            const element = basketItems.data[index];
            total = total + element.price
        }
        return total;
    }

  return (
    <>
    <section className="py-24 relative">
    <div className="w-full max-w-7xl px-4 md:px-5 lg-6 mx-auto">

        <h2 className="title font-manrope font-bold text-4xl leading-10 mb-8 text-center text-black">Shopping Cart
        </h2>
        <div className="hidden lg:grid grid-cols-2 py-6">
            <div className="font-normal text-xl leading-8 text-gray-500">Product</div>
            <p className="font-normal text-xl leading-8 text-gray-500 flex items-center justify-between">
                <span className="w-full max-w-[200px] text-center">Total</span>
            </p>
        </div>
    {
        basketItems && basketItems.data && basketItems.data.map((value:any,key:any) => (
<div className="grid grid-cols-1 lg:grid-cols-2 min-[550px]:gap-6 border-t border-gray-200 py-6">
            <div
                className="flex items-center flex-col min-[550px]:flex-row gap-3 min-[550px]:gap-6 w-full max-xl:justify-center max-xl:max-w-xl max-xl:mx-auto">
                {/* <div className="img-box"><img src="https://pagedone.io/asset/uploads/1701162850.png" alt="perfume bottle image" className="xl:w-[140px]"></div> */}
                <div className="pro-data w-full max-w-sm ">
                    <h5 className="font-semibold text-xl leading-8 text-black max-[550px]:text-center"> {value.gameName}
                    </h5>
                    <p
                        className="font-normal text-lg leading-8 text-gray-500 my-2 min-[550px]:my-3 max-[550px]:text-center">
                        {value.gameAuthor}  </p>
                    <h6 className="font-medium text-lg leading-8 text-indigo-600  max-[550px]:text-center">$ {value.price} </h6>
                </div>
            </div>
            <div
                className="flex items-center flex-col min-[550px]:flex-row w-full max-xl:max-w-xl max-xl:mx-auto gap-2">
               
                <h6
                    className="text-indigo-600 font-manrope font-bold text-2xl leading-9 w-full max-w-[176px] text-center">
                    $ {value.price} </h6>
            </div>
        </div>
        ))
    }
        
        <div className="bg-gray-50 rounded-xl p-6 w-full mb-8 max-lg:max-w-xl max-lg:mx-auto">
          
            <div className="flex items-center justify-between w-full py-6">
                <p className="font-manrope font-medium text-2xl leading-9 text-gray-900">Total</p>
                <h6 className="font-manrope font-medium text-2xl leading-9 text-indigo-500">$ {basketItems && basketItems.data &&  totalPrice()}</h6>
            </div>
        </div>
        <div className="flex items-center flex-col sm:flex-row justify-center gap-3 mt-8">
            <button
                className="rounded-full py-4 w-full max-w-[280px]  flex items-center bg-indigo-50 justify-center transition-all duration-500 hover:bg-indigo-100">
                <span className="px-2 font-semibold text-lg leading-8 text-indigo-600">Add Coupon Code</span>
                <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 22 22" fill="none">
                    <path d="M8.25324 5.49609L13.7535 10.9963L8.25 16.4998" stroke="#4F46E5" stroke-width="1.6"
                        stroke-linecap="round" stroke-linejoin="round" />
                </svg>
            </button>


            <CheckoutButton></CheckoutButton>

          
        </div>
    </div>
</section>
</>      
  )
}
