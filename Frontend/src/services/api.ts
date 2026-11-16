import { config } from "../config.ts"

// ==================================
//          Error Handling
// ==================================

async function getResponseErrorMessage(response: Response): Promise<string> {
    const contentType = response.headers.get("content-type") || ""
    if (contentType.includes("application/json")) {
        try {
            const data = await response.json()
            if (data == null) {
                return response.statusText
            }
            return data?.title || data?.message || data?.error || JSON.stringify(data)
        } catch {
            return response.statusText
        }
    }

    try {
        const text = await response.text()
        return text || response.statusText
    } catch {
        return response.statusText
    }
}

export function getErrorMessage(error: unknown): string {
    if (error instanceof Error) {
        return error.message
    }
    if (typeof error === "string") {
        return error
    }
    return "An unexpected error occurred"
}

// =====================================
//          Response Handling
// =====================================

async function handleResponse(response: Response): Promise<Response> {
    if (response.ok) {
        return response
    }

    const message = await getResponseErrorMessage(response)

    if (response.status === 400) {
        throw new Error(`Bad Request: ${message}`)
    }

    if (response.status === 401 || response.status === 403) {
        throw new Error("Failed to authorize")
    }

    throw new Error(message)
}

// ========================================
//          Abstracted Api Calls
// 
// Predefined type of returned object like in types/ExampleType.ts 
// returns an object of that type or a response
// 
// Available methods: Get, Delete, Put, Post
// 
// usage:
// 
// - apiGet<ExampleType>("/Example/Api/Endpoint", { "page": 1 })
//          ^^^^^^^^^^^   ^^^^^^^^^^^^^^^^^^^^^    ^^^^^^^^^^^
//         returned type    API endpoint url     Query JSON Object (optional)
// 
// 
// - apiPost<PostedObjectType>("/Example/Api/Endpoint", { "taskId": 102, "content": "1+1" } { "page": 1 })
//           ^^^^^^^^^^^^^^^^                            ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//           body object type                             body of the modified/inserted data
// ========================================

export async function apiGet<Type>(endpoint: string, options: any = {}): Promise<Type> 
{
    console.log("Fetched:" + endpoint)
    const response = await fetch(config.API_URL.concat(endpoint).concat("?").concat(new URLSearchParams(options).toString()), {
        credentials: "include"
    })

    await handleResponse(response)

    return response.json() as Type
}

export async function apiDelete(endpoint: string, options: any = {}): Promise<Response> 
{
    console.log("Deleted:" + endpoint)
    const response = await fetch(config.API_URL.concat(endpoint).concat("?").concat(new URLSearchParams(options).toString()), {
        method: 'DELETE',
        credentials: "include"
    })

    return await handleResponse(response)
}

export async function apiPut<Type>(endpoint: string, body: Type | {} = {}, options: any = {}): Promise<Response> 
{
    console.log("Edited:" + endpoint)
    const response = await fetch(config.API_URL.concat(endpoint).concat("?").concat(new URLSearchParams(options).toString()), {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body),
        credentials: "include"
    })

    return await handleResponse(response)
}

export async function apiPost<Type>(endpoint: string, body: Type | {} = {}, options: any = {}): Promise<Response> 
{
    console.log("Posted:" + endpoint)
    const response = await fetch(config.API_URL.concat(endpoint).concat("?").concat(new URLSearchParams(options).toString()), {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body),
        credentials: "include"
    })

    return await handleResponse(response)
}