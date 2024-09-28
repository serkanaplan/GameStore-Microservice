'use server'

import { fetchProccess } from "@/app/library/fetchProcess";





export async function GetMyOrder()
{
    return await fetchProccess.get('order');
}


