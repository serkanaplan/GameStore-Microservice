import { headers } from "next/headers";
import { getTokenWorkarround } from "../authActions/authNext";


const baseUrl = "http://localhost:6001/";

async function get(url:string)
{
    var result = await getHeaders()
    const requestSettings = {
        method : 'GET',
        headers : await getHeaders()
    }
    const response = await fetch(baseUrl+url,requestSettings);
    return await throwResponse(response);
}


async function post(url:string,body:{})
{
    const requestOptions = {
        method:'POST',
        headers:await getHeaders(),
        body:JSON.stringify(body)
    }
    const response = await fetch(baseUrl+url,requestOptions);
    return await throwResponse(response);
}

async function postForm(url:string,body:any)
{
    const requestOptions = {
        method:'POST',
        headers:await getHeadersForm(),
        body:body
    }
    console.log(requestOptions.body)
    const response = await fetch(baseUrl+url,requestOptions);
    return await throwResponse(response);
}




async function postEmpty(url:string)
{
    const requestOptions = {
        method:'POST',
        headers:await getHeaders(),
    }
    const response = await fetch(baseUrl+url,requestOptions);
    return await throwResponse(response);
}


async function put(url:string,body:{})
{
    const requestOptions = {
        method:'PUT',
        headers:await getHeaders(),
        body:JSON.stringify(body)
    }
    const response = await fetch(baseUrl+url,requestOptions);
    return await throwResponse(response);
}

async function del(url:string)
{
    const requestOptions = {
        method:'DELETE',
        headers:await getHeaders(),

    }
    const response = await fetch(baseUrl+url,requestOptions);
    return await throwResponse(response);
}

async function getHeadersForm(): Promise<Record<string, string>> {
    const token = await getTokenWorkarround();
    const headers: Record<string, string> = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token.access_token;
    }
    return headers;
}


async function getHeaders()
{
        const token = await getTokenWorkarround();
        const headers = {'Content-type':'application/json'} as any;
        if (token) {
            headers.Authorization = 'Bearer '+token.access_token
        }
        return headers;
}



async function throwResponse(response:Response)
{
    const text = await response.text();

    let data;

    try {
        data = JSON.parse(text);

    } catch (error) {
        data = text;    
    }

    if (response.ok) {
        return data || response.statusText;
    }
    else {
        const error = {
            status:response.status,
            message:typeof data === 'string' && data.length > 0 ? data : response.statusText
        }

        return {error}
    }

}
export const fetchProccess = {
    get,
    post,
    del,
    put,
    postEmpty,
    postForm
}