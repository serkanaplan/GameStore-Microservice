import React from 'react'
import { GrGamepad } from 'react-icons/gr';
import LoginButton from './LoginButton';
import { getCurrentUser } from '../authActions/authNext';
import AccounActions from './AccounActions';
import Link from 'next/link';
import NavLogo from './NavLogo';
import CartIcon from './CartIcon';

async function NavigationBar() {
    const user = await getCurrentUser();
    console.log("User Details")
    console.log(user)
    
  return (
    <header className='sticky top-o z-50 flex justify-between bg-slate-600 p-5 items-center text-gray-1000 shadow-md' >
          <NavLogo></NavLogo>
            <div>Categories</div>
           <CartIcon></CartIcon>
            <div>
              {
                  user ? ( <AccounActions user={user} ></AccounActions> ) : ( <LoginButton></LoginButton> )
              }
            </div>
    </header>
  )
}

export default NavigationBar