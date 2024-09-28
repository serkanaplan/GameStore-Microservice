import { NextAuthOptions } from "next-auth";
import NextAuth from "next-auth/next";
import DuendeIdentityServer6 from "next-auth/providers/duende-identity-server6";

export const authenticationSettings:NextAuthOptions = {
    session:{
        strategy:'jwt'
    },
    providers:[
        DuendeIdentityServer6({
            id:'id-server',
            clientId:'frontend',
            clientSecret:'BigSecret',
            issuer:'http://localhost:5001',
            authorization:{params:{scope:'openid profile microserviceApp'}},
            idToken:true
        })
    ],
    callbacks:{
       
        async jwt({token,profile,account}){
               
                if (profile) {
                    token.username = profile.username
                    token.role = profile.role
                }

                if (account) {
                    token.access_token = account.access_token
                } 
                return token;
        },
        async session({session,token}){
            if (token) {
                session.user.username = token.username
                session.user.role = token.role
            }
            return session;
        },
    }
}


const handler = NextAuth(authenticationSettings);
export {handler as GET,handler as POST,handler as PUT}