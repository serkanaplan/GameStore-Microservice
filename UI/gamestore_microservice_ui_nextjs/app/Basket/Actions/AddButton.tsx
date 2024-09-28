'use client'
import { addBasketGame, getBasketItems } from '@/app/api/basket/basketActions'
import { Button } from '@nextui-org/react'
import React from 'react'
import Cart from '../Details/page'
import { useStore } from '@/app/HooksManagement/basketItemState'

export default function AddButton(data:any) {

  const updateItemsCount = useStore((state:any) => state.updateBasketCount);

    const addItem = async () => {
        const gameItem = {
            gameId:data.data.id,
            gameName:data.data.gameName,
            gameAuthor:data.data.gameAuthor,
            price:data.data.price,
            gameDescription:data.data.gameDescription
        }


        await addBasketGame(gameItem);
        var response = await getBasketItems();
        updateItemsCount(response.data.length)
    }


  return (
    <Button className="flex ml-auto text-white bg-red-500 border-0 py-2 px-6 focus:outline-none hover:bg-red-600 rounded" onClick={addItem} title='deneme' >Add To Cart</Button>
  )
}
