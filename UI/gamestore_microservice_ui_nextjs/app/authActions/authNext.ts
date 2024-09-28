'use server'
import { getServerSession } from "next-auth";
import { authenticationSettings } from "../api/auth/[...nextauth]/route";
import { cookies, headers } from "next/headers";
import { NextApiRequest } from "next";
import { getToken } from "next-auth/jwt";
export async function getSession(){
    return await getServerSession(authenticationSettings);
}


export async function getCurrentUser()
{
    const session = await getSession();
    if (!session) {
        return null;
    }
    console.log("session.user")
    console.log(session.user)
    return session.user;
}

export async function getTokenWorkarround()
{
    const req = {
        headers:Object.fromEntries(headers() as Headers),
        cookies:Object.fromEntries(
            cookies().getAll().map(x=>[x.name,x.value])
        )
    }as NextApiRequest;

    return await getToken({req});
}