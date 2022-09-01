import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getApi, postApi } from "../Clients/requests";
import Google from './Google';
import { Link } from "react-router-dom";

const UserForm = ({ type, setName }) => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [emailError,setEmailError] = useState(false);
  const [usernameError,setUserNameError] = useState(false);
  const [passwordError,setPasswordError] = useState(false);
  const [usernameTaken,setUsernameTaken] = useState(false);
  const [clientId, setClientId] = useState('');
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const emailRegex = /\S+@\S+\.\S+/;

  useEffect(() => {
    getApi('/account/client-id').then(response => {
      setClientId(response);
      setLoading(false);
    })
  }, []);

  const validateForm = (emailRegex)=>{
    let isPasswordValid = false;
    let isUserNameValid = false;
    let isEmailValid = false;
    if(password===''){
      setPasswordError(true);
    }else{
      isPasswordValid = true;
      setPasswordError(false);
    }
    if(username===''){
      setUserNameError(true);
    }else{
      isUserNameValid = true;
      setUserNameError(false);
    }
    if(email===''|| !emailRegex.test(email)){
      setEmailError(true);
    }else{
      isEmailValid = true;
      setEmailError(false);
    }
    if(isPasswordValid&&isUserNameValid&&isEmailValid){
      return true;
    }
  }

  const submit = (e) => {
    e.preventDefault();
    if (type === 'login') {
      const body = {
        "username":username,
        "email":'',
        "password":password,
        "role":""
      }
      postApi('/account/login', body).then((response)=>response.json()).then(data => {
        sessionStorage.setItem('token', data.token);
        sessionStorage.setItem('role',data.role);
        setName(username);
      }).then(() => navigate('/calendar'))}
    if(type ==='registration'){
      if(validateForm(emailRegex)){
        postApi("/account/checkname",username).then((response)=>response.json()).then((result)=>{
          if(result!==true){
            const body = {
              "username":username,
              "email":email,
              "password":password,
              "role":"User"
            }
            postApi("/account/registration", body).then((response)=>{
              if(response.status ===200){
                navigate("/");
              }
            })
          }else{
            setUsernameTaken(true);
          }
        })
      }
    }
  }

  return (
    <div className="container form-control">
    <form>
      <div>
        <label className="form-label">Username</label><br/>
        <input className="form-control" type="text" onChange={(e) => setUsername(e.target.value)} />
        {usernameError===true?<p id="userName" style={{'color':'red'}}>Please give a Username!</p>:<></>}
        {usernameTaken===true?<p id="userName" style={{'color':'red'}}>Username is already taken!</p>:<></>}
      </div>
      <br></br>
      {type === 'registration' ? <>
        <div>
          <label className="form-label">Email</label><br/>
          <input className="form-control" type="email" onChange={(e) => setEmail(e.target.value)}/>
          {emailError===true?<p style={{'color':'red'}}>Please provide a valid e-mail address!</p>:<></>}
        </div>
        <br></br>
      </> : <></> }
      <div>
        <label className="form-label">Password</label><br/>
        <input className="form-control" type="password" onChange={(e) => setPassword(e.target.value)}/>
        {passwordError===true?<p style={{'color':'red'}}>Please provide a password!</p>:<></>}
      </div>
      <br></br>
      <div>
        <button className="form-control btn btn-primary" type="submit" onClick={(e) => submit(e)}>{type!=='registration'?"Login":"Register"}</button>
        {loading ? <></>
        :
        <Google clientId={clientId} setName={setName}/>
        }
      </div>
    </form>
    {type==='login'?<div><p>If you forgot your password click <Link to="/forgot">here</Link></p></div>:<></>}
    </div>
  )
}

export default UserForm
