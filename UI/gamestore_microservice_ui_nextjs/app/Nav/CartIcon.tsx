'use client'
import Link from 'next/link'
import React, { useEffect, useState } from 'react'
import { FaShoppingCart } from 'react-icons/fa'
import { getBasketItems } from '../api/basket/basketActions'
import { useStore } from '../HooksManagement/basketItemState'

export default function CartIcon() {
    const [itemsCount,setItemsCount] = useState(0);
    const basketCounter = useStore((state:any) => state.basketCount)

    // const updateCart = async () => {
    //     console.log("trigger update cart")
    //     const items = await getBasketItems();
    //     if (items && items.data) {
    //         console.log(items.data.length)
    //         setItemsCount(items.data.length)
    //     }
    // }


    // useEffect(()=>{
    //     updateCart()
    // },[itemsCount])



  return (
    <div className='flex items-center' >
    <div className='mr-2'>
      <Link href={'/Basket/Details'} >
         <FaShoppingCart size={30}/>
      
      </Link>
    </div>
    <div >
     ({basketCounter})
    </div>
    
</div>
  )
}
