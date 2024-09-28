import React from "react";
import {Card, CardHeader, CardBody, CardFooter, Image, Button} from "@nextui-org/react";
import Link from "next/link";


type Props = {
    game:any
}


export default function GameCard({game}:Props) {
  return (
    <Card className="py-4 px-4 border border-gray-600 rounded-s-lg bg-slate-600">
 <Link href={`Games/Details/${game.id}`} >
    <CardHeader className="pb-0 pt-2 px-4 flex-col items-center">
      <p className="text-tiny uppercase font-bold"> ${game.price} </p>
      <small className="text-default-500"> {game.gameName} </small>
      <h4 className="font-bold text-large"> {game.gameDescription} </h4>
    </CardHeader> 
    <CardBody className="overflow-visible py-2">
      <Image
        alt="Card background"
        className="object-cover rounded-xl"
        src={ game.gameImages.length > 0 ?  game.gameImages[0].imageUrl : `https://i.pinimg.com/originals/c5/ca/ae/c5caae987b65d9e39f6b174d1fd19fae.png`}
        width={270}
      />
       
      
    </CardBody>
    </Link>
     
  </Card>

  )
}
