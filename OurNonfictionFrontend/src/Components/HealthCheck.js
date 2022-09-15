import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import { getApi } from '../Clients/requests'

const HealthCheck = () => {
    const [health,setHealth] = useState(true)

    useEffect(()=>{
        getApi("/account/client-id").then((data)=>{
            if(data !==null){
                setHealth(true)
            }
        }).catch(setHealth(false))
    },[])
        
    return (
    <div>
      {health?<p>Server is online</p>:<p>Server is offline</p>}
    </div>
  )
}

export default HealthCheck
