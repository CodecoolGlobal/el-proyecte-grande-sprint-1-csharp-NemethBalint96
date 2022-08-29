
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
    return await response;
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

export async function fetchPlus(url,retries){
    try{
  let response = await fetch(url);
      if (response.ok) {
        return response.json();
      }
    else{
        throw new Error(response.status);
    }}
      catch(error){
        if(error.response){
            return;
        }else{
            if(retries>0){
                console.log(retries);
                return await new Promise((resolve, reject) => {
                    setTimeout(() => {
                    resolve(fetchPlus(url, retries-1));
                    }, 500);
                });
            }
        }
    }
}
