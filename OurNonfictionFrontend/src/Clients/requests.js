
export async function getApi(url){
    const token = sessionStorage.getItem('token');
    const settings = {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            Authorization:`Bearer ${token}`
        }
        };
    const response = await fetch(url, settings);
    return await response.json();
}

export async function postApi(url,body){    
    const token = sessionStorage.getItem('token');
    const settings = {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            Authorization:`Bearer ${token}`
        },
        body:JSON.stringify(body)
        };
    const response = await fetch(url,settings);
    return response;
}

export async function putApi(url,body){
    const token = sessionStorage.getItem('token');
    const settings = {
        method: 'PUT',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            Authorization:`Bearer ${token}`
        },
        body:JSON.stringify(body)
        };
    return await fetch(url,settings);
}

export async function deleteApi(url){
    const token = sessionStorage.getItem('token');
    const settings = {
        method: 'DELETE',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
            Authorization:`Bearer ${token}`
        }
    };
    const response = await fetch(url,settings);
    return response;
}

export async function fetchPlus(url, retries) {
  try {
    let response = await fetch(url);
    if (response.ok)
      return response.json();

    throw new Error(response.status);
  }
  catch (error) {
    if (error.response)
      return;
    if(retries > 0)
      return await new Promise((resolve, reject) => {
        setTimeout(() => {
          resolve(fetchPlus(url, retries-1));
        }, 500);
      });
  }
}
