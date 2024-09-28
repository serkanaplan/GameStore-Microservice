import React, { cache } from 'react'
import GameCard from './GameCard';


async function GetData()
{
    const res = await fetch('http://localhost:6001/game',{cache:'no-store'});
    if (!res.ok) {
        throw new Error("failed to fetch data");
    }
    return res.json();
}


async function GameList() {

    const data = await GetData();
  return (
    <div className='grid grid-cols-3 gap-10'  >
       {
        data && data.data.map((game:any) => (
          <GameCard game={game} key={game.id} ></GameCard>
        ))
       }
    </div> 
  )
}

export default GameList