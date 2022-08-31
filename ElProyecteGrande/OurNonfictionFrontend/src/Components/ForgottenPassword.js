import React, { useState } from 'react';
import { postApi } from '../Clients/requests';

function ForgottenPassword() {
const [email,setEmail] = useState('');
const [emailError,setEmailError] = useState(false);
function submit(e){
e.preventDefault();
postApi("/account/checkemail",email).then((response)=>response.json()).then((result)=>{
    if(result!==true){
        setEmailError(true);
    }
else{
    setEmailError(false);
    
}})
}
  return (
    <div>
      <div>
          <label className="form-label">Email</label><br/>
          <input className="form-control" type="email" onChange={(e) => setEmail(e.target.value)}/>
          {emailError===true?<p style={{'color':'red'}}>Please provide a valid e-mail address!</p>:<></>}
        </div>
        <div>
        <button className="form-control btn btn-primary" type="submit" onClick={(e) => submit(e)}>Send</button>
      </div>
    </div>
  )
}

export default ForgottenPassword
