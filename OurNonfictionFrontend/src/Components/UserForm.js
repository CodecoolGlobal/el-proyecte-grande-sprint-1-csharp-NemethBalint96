import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { postApi, fetchPlus } from '../Clients/requests'
import Google from './Google'
import { Link } from 'react-router-dom'

const UserForm = ({ type, setName }) => {
  const [username, setUsername] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [emailError, setEmailError] = useState(false)
  const [usernameError, setUserNameError] = useState(false)
  const [passwordError, setPasswordError] = useState(false)
  const [usernameTaken, setUsernameTaken] = useState(false)
  const [clientId, setClientId] = useState('')
  const [loading, setLoading] = useState(true)
  const [loginError, setLoginError] = useState(false)
  const navigate = useNavigate()
  const emailRegex = /\S+@\S+\.\S+/

  useEffect(() => {
    fetchPlus('/account/client-id', 100).then((response) => {
      setClientId(response.result)
      setLoading(false)
    })
  }, [])

  const validateForm = (emailRegex) => {
    let isPasswordValid = false
    let isUserNameValid = false
    let isEmailValid = false
    if (password === '') {
      setPasswordError(true)
    } else {
      isPasswordValid = true
      setPasswordError(false)
    }
    if (username === '') {
      setUserNameError(true)
    } else {
      isUserNameValid = true
      setUserNameError(false)
    }
    if (email === '' || !emailRegex.test(email)) {
      setEmailError(true)
    } else {
      isEmailValid = true
      setEmailError(false)
    }
    if (isPasswordValid && isUserNameValid && isEmailValid) {
      return true
    }
    return false
  }

  const submit = (e) => {
    e.preventDefault()
    setLoading(true)
    if (type === 'login') {
      setLoginError(false)
      const body = {
        username: username,
        email: '',
        password: password,
        role: ''
      }

      setUserNameError(username === '')
      setPasswordError(password === '')
      if (!username || !password) {
        setLoading(false)
        return
      }

      postApi('/account/login', body)
        .then((response) => {
          if (!response.ok) {
            throw response
          }
          return response.json()
        })
        .then((data) => {
          sessionStorage.setItem('token', data.token)
          sessionStorage.setItem('role', data.role)
          sessionStorage.setItem('name', username)
          setName(username)
          navigate('/calendar')
        })
        .catch(() => {
          setLoading(false)
          setLoginError(true)
        })
    }

    if (type === 'registration') {
      if (validateForm(emailRegex)) {
        postApi('/account/checkname', username)
          .then((response) => response.json())
          .then((result) => {
            if (result !== true) {
              const body = {
                username: username,
                email: email,
                password: password,
                role: 'User'
              }
              postApi('/account/registration', body).then((response) => {
                if (response.status === 200) {
                  setLoading(false)
                  navigate('/')
                }
              })
            } else {
              setUsernameTaken(true)
              setLoading(false)
            }
          })
      } else {
        setLoading(false)
      }
    }
  }

  return (
    <>
      {loading ? (
        <div className="loader-container">
          <div className="spinner"></div>
        </div>
      ) : (
        <div className="container form-control" style={{ width: '500px !important' }}>
          <form>
            <div>
              <label className="form-label">Username</label>
              <br />
              <input
                className="form-control"
                type="text"
                onChange={(e) => setUsername(e.target.value)}
              />
              {loginError ? (
                <p style={{ color: 'red' }}>Please provide a valid Username!</p>
              ) : usernameError ? (
                <p id="userName" style={{ color: 'red' }}>
                  Please give a Username!
                </p>
              ) : usernameTaken ? (
                <p id="userName" style={{ color: 'red' }}>
                  Username is already taken!
                </p>
              ) : (
                <br></br>
              )}
            </div>

            {type === 'registration' ? (
              <>
                <div>
                  <label className="form-label">Email</label>
                  <br />
                  <input
                    className="form-control"
                    type="email"
                    onChange={(e) => setEmail(e.target.value)}
                  />
                  {emailError ? (
                    <p style={{ color: 'red' }}>Please provide a valid e-mail address!</p>
                  ) : (
                    <br></br>
                  )}
                </div>
              </>
            ) : (
              <></>
            )}
            <div>
              <label className="form-label">Password</label>
              <br />
              <input
                className="form-control"
                type="password"
                onChange={(e) => setPassword(e.target.value)}
              />
              {loginError ? (
                <p style={{ color: 'red' }}>Please provide a valid Password!</p>
              ) : passwordError ? (
                <p style={{ color: 'red' }}>Please provide a password!</p>
              ) : (
                <br></br>
              )}
            </div>
            <div>
              <button
                className="form-control btn btn-primary"
                type="submit"
                onClick={(e) => submit(e)}>
                {type !== 'registration' ? 'Login' : 'Register'}
              </button>
              <div className="strike">
                <span>Or</span>
              </div>
              {loading ? (
                <></>
              ) : (
                <Google clientId={clientId} setName={setName} setLoading={setLoading} />
              )}
            </div>
          </form>
          {type === 'login' ? (
            <div>
              <p>
                If you forgot your password click <Link to="/forgot">here</Link>
              </p>
            </div>
          ) : (
            <></>
          )}
        </div>
      )}
    </>
  )
}

export default UserForm
