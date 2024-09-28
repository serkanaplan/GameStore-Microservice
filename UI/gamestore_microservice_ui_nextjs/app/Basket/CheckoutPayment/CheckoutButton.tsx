'use client'
import { checkoutBasketItem } from '@/app/api/basket/basketActions'
import { Button } from 'flowbite-react'
import { useRouter } from 'next/navigation';
import React from 'react'

export default function CheckoutButton() {
    const router = useRouter();


    const goToCheckout = async () => {

        const response = await checkoutBasketItem();
        if (response.isSuccess) {
            router.push('/Order/OrderDetail');
        }

    }


  return (
    <div>
          <Button  onClick={goToCheckout}
                className="rounded-full w-full max-w-[280px] py-4 text-center justify-center items-center bg-indigo-600 font-semibold text-lg text-white flex transition-all duration-500 hover:bg-indigo-700">Continue
                to Payment
                <svg className="ml-2" xmlns="http://www.w3.org/2000/svg" width="23" height="22" viewBox="0 0 23 22"
                    fill="none">
                    <path d="M8.75324 5.49609L14.2535 10.9963L8.75 16.4998" stroke="white" stroke-width="1.6"
                        stroke-linecap="round" stroke-linejoin="round" />
                </svg>
            </Button>
    </div>
  )
}
