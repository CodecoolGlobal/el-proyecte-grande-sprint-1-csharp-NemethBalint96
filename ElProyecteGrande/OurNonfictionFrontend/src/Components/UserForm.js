import { useState } from 'react';
import { postApi } from "../Clients/requests";

const UserForm = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const submit = (e) => {
    e.preventDefault();
    const body = {
      "username":username,
      "email":email,
      "password":password,
      "role":"User"
    }
    postApi("/account/registration", body)
  } 

  return (
    <div className="container form-control">
    <form>
      <div>
        <label className="form-label">Username</label><br/>
        <input className="form-control" type="text" onChange={(e) => setUsername(e.target.value)} />
      </div>
      <br></br>
      <div>
        <label className="form-label">Email</label><br/>
        <input className="form-control" type="email" onChange={(e) => setEmail(e.target.value)}/>
      </div>
      <br></br>
      <div>
        <label className="form-label">Password</label><br/>
        <input className="form-control" type="password" onChange={(e) => setPassword(e.target.value)}/>
      </div>
      <br></br>
      <div>
        <input className="form-control btn btn-primary" type="submit" onClick={(e) => submit(e)}/>
      </div>
    </form>
    </div>
  )
}

export default UserForm
