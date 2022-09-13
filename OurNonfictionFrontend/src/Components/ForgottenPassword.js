import React, { useState } from 'react'
import { postApi } from '../Clients/requests'
import { useNavigate, useParams } from 'react-router-dom'

function ForgottenPassword({ type }) {
  const [email, setEmail] = useState('')
  const [emailError, setEmailError] = useState(false)
  const [password, setPassword] = useState('')
  const [passwordError, setPasswordError] = useState(false)
  const navigate = useNavigate()
  const params = useParams()
  const username = params.username

  function submit(e) {
    e.preventDefault()
    if (type === 'sendemail') {
      postApi('/account/checkemail', email)
        .then((response) => response.json())
        .then((result) => {
          if (result !== true) {
            setEmailError(true)
          } else {
            setEmailError(false)
            postApi('/account/passwordchange', email).then(navigate('/'))
          }
        })
    } else {
      if (password === '' || password === null) {
        setPasswordError(true)
      } else {
        postApi(`/account/passwordchange/${username}`, password).then(navigate('/'))
      }
    }
  }
  return (
    <div className="form-control container" style={{ width: '500px !important' }}>
      {type === 'sendemail' ? (
        <div>
          <label className="form-label">Email</label>
          <br />
          <input className="form-control" type="email" onChange={(e) => setEmail(e.target.value)} />
          {emailError === true ? (
            <p style={{ color: 'red' }}>Please provide a valid e-mail address!</p>
          ) : (
            <></>
          )}
        </div>
      ) : (
        <div>
          <label className="form-label">Password</label>
          <br />
          <input
            className="form-control"
            type="password"
            onChange={(e) => setPassword(e.target.value)}
          />
          {passwordError === true ? (
            <p style={{ color: 'red' }}>Please give a new not empty password!</p>
          ) : (
            <></>
          )}
        </div>
      )}
      <br></br>
      <div>
        <button className="form-control btn btn-primary" type="submit" onClick={(e) => submit(e)}>
          Send
        </button>
      </div>
    </div>
  )
}

export default ForgottenPassword
