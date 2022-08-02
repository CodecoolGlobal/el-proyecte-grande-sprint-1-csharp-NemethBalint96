
export async function getApi(url){
    const response = await fetch(url);
    return await response.json();
}