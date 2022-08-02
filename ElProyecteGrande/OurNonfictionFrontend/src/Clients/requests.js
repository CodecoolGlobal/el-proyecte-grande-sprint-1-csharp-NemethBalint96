
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

