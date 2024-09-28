import React from 'react'
import { FetchMyGames } from '../api/game/gameActions'

export default async function page() {

    var response = await FetchMyGames();



    return (  
        <>
            {
               response.data && response.data.map((mygame:any, key:any) => (
        <ul className="bg-white shadow overflow-hidden sm:rounded-md max-w-sm mx-auto mt-16">

                    <li key={key} className='mb-6'>
                        <div className="px-4 py-5 sm:px-6">
                            <div className="flex items-center justify-between">
                                <h3 className="text-lg leading-6 font-medium text-gray-900">{mygame.gameName}</h3>
                                <p className="mt-1 max-w-2xl text-sm text-gray-500">{mygame.gameDescription}</p>
                            </div>
                            <div className="mt-4 flex items-center justify-between">
                                <p className="text-sm font-medium text-gray-500">Status: <span className="text-green-600">Active</span></p>
                                <a href={mygame.gameInfo.toString()} className="font-medium text-indigo-600 hover:text-indigo-500" download>Download Game</a>
                            </div>
                        </div>
                    </li>
        </ul>

                ))
            }
            </>
    )
    
}
