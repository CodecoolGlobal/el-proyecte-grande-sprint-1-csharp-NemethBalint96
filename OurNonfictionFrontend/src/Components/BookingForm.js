import { useState, useEffect } from 'react'
import { getApi, postApi, putApi } from '../Clients/requests'
import { useNavigate, useParams } from 'react-router-dom'
import { addDays } from 'date-fns'

let now = new Date()
let baseDate = now.toISOString().slice(0, 10)
let tomorrow = now.setDate(now.getDate() + 1)
let sec = tomorrow
let normalDate = new Date(sec).toISOString().slice(0, 10)

const BookingForm = () => {
  const navigate = useNavigate()
  const [bookersName, setBookersName] = useState('')
  const [email, setEmail] = useState('')
  const [country, setCountry] = useState('')
  const [adults, setAdults] = useState(0)
  const [minAdults, setMinAdults] = useState(0)
  const [children, setChildren] = useState(0)
  const [minChildren, setMinChildren] = useState(0)
  const [infants, setInfants] = useState(0)
  const [minInfants, setMinInfants] = useState(0)
  const [arrivalDate, setArrivalDate] = useState(baseDate)
  const [departureDate, setDepartureDate] = useState(normalDate)
  const [guests, setGuests] = useState([])
  const [loading, setLoading] = useState(false)
  const emailRegex = /\S+@\S+\.\S+/
  const [emailError, setEmailError] = useState({})
  let isValidEmail = false

  const params = useParams()
  const bookingId = params.bookingId > 0 ? params.bookingId : null
  const body = {
    bookersName: bookersName,
    email: email,
    country: country,
    adults: adults,
    children: children,
    infants: infants,
    arrivalDate: arrivalDate,
    departureDate: departureDate,
    guests: guests
  }

  useEffect(() => {
    if (bookingId) {
      setLoading(true)
      getApi(`/bookingapi/${bookingId}`)
        .then((data) => {
          setBookersName(data.bookersName)
          setEmail(data.email)
          setCountry(data.country)
          setAdults(data.adults)
          setMinAdults(data.adults)
          setChildren(data.children)
          setMinChildren(data.children)
          setInfants(data.infants)
          setMinInfants(data.infants)
          setArrivalDate(data.arrivalDate.slice(0, 10))
          setDepartureDate(data.departureDate.slice(0, 10))
          setGuests(data.guests)
          setLoading(false)
        })
        .catch(() => {
          navigate('/error')
        })
    }
  }, [bookingId])

  const onclick = (e) => {
    setLoading(true)
    validateEmail()
    e.preventDefault()
    if (!bookingId && isValidEmail) {
      postApi('bookingapi', body)
        .then((data) => data.json())
        .then((data) => {
          setLoading(false)
          navigate(`/available/${data.id}`)
        })
        .catch(() => {
          navigate('/error')
        })
    } else if (isValidEmail) {
      putApi(`/bookingapi/${bookingId}`, body)
        .then((response) => {
          if (response.status === 200) {
            setLoading(false)
            navigate(`/booking/${bookingId}`)
          }
        })
        .catch(() => {
          navigate('/error')
        })
    }

    function validateEmail() {
      if (emailRegex.test(email) || email === '') {
        isValidEmail = true
        setEmailError(false)
      } else {
        setLoading(false)
        setEmailError(true)
      }
    }
  }

  function getTomorrowsDate(e) {
    let targetDate = e.target.value
    let date = new Date(targetDate) //converts IsoString to date object
    let ms = date.getTime(targetDate) // convert date to milliseconds
    let result = ms + 86400000 // add one day in milliseconds to date
    if (targetDate >= departureDate) {
      setDepartureDate(new Date(result).toISOString().slice(0, 10)) //setting departureDate.
    }
  }

  function resetArrivalDate(e) {
    setDepartureDate(e.target.value)
    const departure = new Date(e.target.value)
    const arrival = new Date(arrivalDate)
    if (departure <= arrival) {
      const newArrival = addDays(departure, -1)
      setArrivalDate(newArrival.toISOString().slice(0, 10))
    }
  }

  return (
    <>
      {loading ? (
        <div className="loader-container">
          <div className="spinner"></div>
        </div>
      ) : (
        <div className="container form-control">
          <form>
            <div className="row">
              <div className="col">
                <label className="form-label">Booker&apos;s Name</label>
                <br />
                <input
                  className="form-control"
                  type="text"
                  value={bookersName}
                  onChange={(e) => setBookersName(e.target.value)}
                />
              </div>
              <div className="col">
                <label className="form-label">Email</label>
                <br />
                <input
                  className="form-control"
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
                {emailError === true ? (
                  <p style={{ color: 'red' }}>Please provide a valid e-mail address!</p>
                ) : (
                  <></>
                )}
              </div>
              <div className="col">
                <label className="form-label">Country</label>
                <br />
                <input
                  className="form-control"
                  type="text"
                  value={country}
                  onChange={(e) => setCountry(e.target.value)}
                />
              </div>
            </div>
            <br></br>
            <div className="row">
              <div className="col">
                <label className="form-label">Adults</label>
                <br />
                <input
                  className="form-control"
                  type="number"
                  min={minAdults}
                  value={adults}
                  onChange={(e) => setAdults(e.target.value)}
                />
              </div>
              <div className="col">
                <label className="form-label">Children</label>
                <br />
                <input
                  className="form-control"
                  type="number"
                  min={minChildren}
                  value={children}
                  onChange={(e) => setChildren(e.target.value)}
                />
              </div>
              <div className="col">
                <label className="form-label">Infants</label>
                <br />
                <input
                  className="form-control"
                  type="number"
                  min={minInfants}
                  value={infants}
                  onChange={(e) => setInfants(e.target.value)}
                />
              </div>
            </div>
            <br></br>
            <div className="row">
              <div className="col">
                <label className="form-label">Arrival&apos;s Date</label>
                <br />
                <input
                  className="form-control"
                  type="date"
                  min={baseDate}
                  value={arrivalDate}
                  onChange={(e) => {
                    setArrivalDate(e.target.value)
                    getTomorrowsDate(e) // sets departureDate to 1 day later
                  }}
                />
              </div>
              <div className="col">
                <label className="form-label">Departure&apos;s Date</label>
                <br />
                <input
                  className="form-control"
                  type="date"
                  min={normalDate}
                  value={departureDate}
                  onChange={(e) => resetArrivalDate(e)}
                />
              </div>
            </div>
            <br></br>
            <br></br>
            <div>
              <input className="form-control btn btn-primary" type="submit" onClick={onclick} />
            </div>
          </form>
        </div>
      )}
    </>
  )
}

export default BookingForm
