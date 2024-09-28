import React from 'react'
import { getCurrentUser } from '../authActions/authNext'

export default async function page() {
    const user = await getCurrentUser();
  return (
    <div>
        {JSON.stringify(user,null,2)}
    </div>
  )
}
