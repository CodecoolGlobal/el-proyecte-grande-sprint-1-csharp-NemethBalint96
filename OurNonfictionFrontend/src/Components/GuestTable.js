import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { getApi } from '../Clients/requests'
import Table from './Table'

const GuestTable = () => {
  const navigate = useNavigate()
  const url = 'guestapi'
  const [guests, setGuests] = useState([])
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    setLoading(true)
    getApi(url)
      .then((data) => {
        setGuests(data)
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
          <Table data={guests} />
        </div>
      )}
    </>
  )
}

export default GuestTable
