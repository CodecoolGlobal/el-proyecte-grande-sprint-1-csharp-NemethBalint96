import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { postApi } from "../Clients/requests";

const UserForm = ({ type, setName }) => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [emailError,setEmailError] = useState(false);
  const [usernameError,setUserNameError] = useState(false);
  const [passwordError,setPasswordError] = useState(false);
  const [usernameTaken,setUsernameTaken] = useState(false);
  const navigate = useNavigate();
  const emailRegex = /\S+@\S+\.\S+/;

  const validateForm = (emailRegex)=>{
    let flag1 = false;
    let flag2 = false;
    let flag3 = false;
    if(password===''){
      setPasswordError(true);
    }else{
      flag1 = true;
      setPasswordError(false);
    }
    if(username===''){
      setUserNameError(true);
    }else{
      flag2 = true;
      setUserNameError(false);
    }
    if(email===''|| !emailRegex.test(email)){
      setEmailError(true);
    }else{
      flag3 = true;
      setEmailError(false);
    }
    if(flag1&&flag2&&flag3){
      return true;
    }
  }

  const submit = (e) => {
    e.preventDefault();
    if (type === 'login') {
      const body = {
        "username":username,
        "email":email,
        "password":password,
        "role":"User"
      }
      postApi('/account/login', body).then((response)=>response.json()).then(data => {
        sessionStorage.setItem('token', data);
        setName(username);
      }).then(() => navigate('/calendar'))}
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
      </div>
    </form>
    </div>
  )
}

export default UserForm
