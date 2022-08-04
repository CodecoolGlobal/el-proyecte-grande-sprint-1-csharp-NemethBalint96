
export async function getApi(url){
    const response = await fetch(url);
    return await response.json();
}

export async function postApi(url,body){
    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body:JSON.stringify(body)
        };
    const response = await fetch(url,settings);
    const data = await response.json();
    return data;
}

export async function putApi(url,body){
    const settings = {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body:JSON.stringify(body)
        };
    return await fetch(url,settings);
}

export async function deleteApi(url){
    const settings = {
        method: 'DELETE',
        };
    const response = await fetch(url,settings);
    return response;
}
