'use client'
import { Button, Dropdown } from 'flowbite-react'
import { User } from 'next-auth'
import { signOut } from 'next-auth/react'
import Link from 'next/link'
import React from 'react'

// type Props = {
//   user:Partial<User>
// }




export default function AccounActions({user}:any) {

  return (
    <div className="flex items-center gap-4">
    <Dropdown label={`Hello bros ${user.name}`} size="sm">
      <Dropdown.Item>
          <Link href='/MyGames' >
            <h1 style={{color:"black"}} >
            My Games

            </h1>
          </Link>
      </Dropdown.Item>
      {
        user.role === "SuperAdmin" && (
          <Dropdown.Item>
          <Link href='User/CreateGame' >
          <h1 style={{color:"black"}} >
            Create Game

            </h1>
          </Link>
      </Dropdown.Item>
        )
      }
    
      <Dropdown.Item>
        <Link href='/MyAccount' >
        <h1 style={{color:"black"}} >
          My Account
          </h1>
        </Link>
      </Dropdown.Item>
      <Dropdown.Item onClick={()=>signOut({callbackUrl:"/"})} >
      <h1 style={{color:"black"}} >
        Sign Out
        </h1>
      </Dropdown.Item>
    </Dropdown>
   
  </div> 
  )
}
