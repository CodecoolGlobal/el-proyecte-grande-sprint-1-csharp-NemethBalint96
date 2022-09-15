import { useState, useEffect } from 'react'
import { getApi } from '../Clients/requests'
import { Link } from 'react-router-dom'
import Table from './Table'

const MainPage = () => {
  const url = 'bookingapi'
  const [firstBooking, setFirstBookings] = useState([])
  const [bookings, setBookings] = useState([])
  const [loading, setLoading] = useState(false)

  const handleSearch = (event) => {
    let search = event.target.value
    const data = bookings.filter((item) =>
      item.bookersName.toLowerCase().includes(search.toLowerCase())
    )
    setBookings(data)
    if (search === '') {
      setBookings(firstBooking)
    }
  }

  useEffect(() => {
    setLoading(true)
    getApi(url)
      .then((data) => {
        data = data.map((booking) => (booking = { ...booking, total: `${booking.total} $` }))
        console.log(data)
        setFirstBookings(data)
        setLoading(false)
        setBookings(data)
      })
      .catch(() => {
        navigate('/error')
      })
  }, [url])

  return (
    <>
      <div className=" container-fluid row justify-content-between">
        <div className="col-auto">
          <input
            className="form-control"
            type="text"
            id="myInput"
            onChange={(e) => {
              handleSearch(e)
            }}
            placeholder="Start typing a name..."
          />
        </div>
        <div className="col-auto">
          <Link to="/newbooking">
            <button className="btn btn-primary">Add New Booking</button>
          </Link>
        </div>
      </div>

      <br></br>
      {loading ? (
        <div className="loader-container">
          <div className="spinner"></div>
        </div>
      ) : bookings !== undefined ? (
        <Table data={bookings} type="Booking" />
      ) : (
        <></>
      )}
    </>
  )
}

export default MainPage
