import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { getApi } from '../Clients/requests'

function Users() {
  const navigate = useNavigate()
  const url = '/account'
  const [accounts, setAccounts] = useState([])
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    setLoading(true)
    getApi(url)
      .then((data) => {
        setAccounts(data)
        setLoading(false)
      })
      .catch(() => {
        navigate('/error')
      })
  }, [url])

  return (
    <>
      {loading ? (
        <div className="loader-container">
          <div className="spinner"></div>
        </div>
      ) : (
        <div>
          <ul>
            {accounts.map((account) => (
              <li key={account}>
                <p>Username: {account.username}</p> <p>Email: {account.email}</p>
                <p>Role: {account.role}</p>
              </li>
            ))}
          </ul>
        </div>
      )}
    </>
  )
}

export default Users
