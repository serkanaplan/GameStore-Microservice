import { DefaultSession } from "next-auth";


declare module 'next-auth'{
    interface Session{
        user:{
            username:string,
            role:string
        } & DefaultSession['user']
    }

    interface Profile{
        username:string,
        role:string
    }
}


declare module 'next-auth/jwt'{
    interface JWT{
        username:string,
        role:string,
        access_token?:string
    }
}