'use server'
import { fetchProccess } from "@/app/library/fetchProcess";



export async function paymentAction(form:any)
{
    return await fetchProccess.post('payment',form);
}

