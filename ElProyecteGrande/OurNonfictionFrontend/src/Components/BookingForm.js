import { useState, useEffect } from "react"
import { getApi, postApi, putApi } from "../Clients/requests"
import { useNavigate, useParams } from "react-router-dom"

let now = new Date();

let baseDate= now.toISOString().slice(0, 10);

let tomorrow = now.setDate(now.getDate()+1);
let sec = tomorrow;
let normalDate = new Date(sec).toISOString().slice(0,10);

const BookingForm = () => {
  const navigate = useNavigate()
  const [bookersName, setBookersName] = useState('')
  const [email, setEmail] = useState('')
  const [country, setCountry] = useState('')
  const [adults, setAdults] = useState(0)
  const [children, setChildren] = useState(0)
  const [infants, setInfants] = useState(0)
  const [arrivalDate, setArrivalDate] = useState(baseDate)
  const [departureDate, setDepartureDate] = useState(normalDate)

  const params = useParams()
  const bookingId = params.bookingId > 0 ? params.bookingId : null
  console.log(bookingId)
  const body = {
    "bookersName":bookersName,
    "email":email,
    "country":country,
    "adults":adults,
    "children":children,
    "infants":infants,
    "arrivalDate":arrivalDate,
    "departureDate":departureDate,
  }

  useEffect(() => {
    if(bookingId) {
      getApi(`/booking/${bookingId}`).then(data => {
        setBookersName(data.bookersName)
        setEmail(data.email)
        setCountry(data.country)
        setAdults(data.adults)
        setChildren(data.children)
        setInfants(data.infants)
        setArrivalDate(data.arrivalDate.slice(0, 10))
        setDepartureDate(data.departureDate.slice(0, 10))
      })
    }
  }, [bookingId])

  const onclick = (e) => {
    console.log(body)
    e.preventDefault()
    if(!bookingId) {
      postApi("booking", body).then(data => {
        navigate(`/available/${data.id}`)
      })
    } else {
      body.id = parseInt(bookingId)
      putApi(`/booking/${bookingId}`, body).then((response) => {
        if(response.status === 204) {
          navigate(`/booking/${bookingId}`)
        }
      })
    }
  }

  function getTomorrowsDate(e) {
    let targetDate = e.target.value;
    let date = new Date(targetDate); //converts IsoString to date object
    let ms = date.getTime(targetDate); // convert date to milliseconds
    let result = ms + 86400000; // add one day in milliseconds to date
    setDepartureDate(new Date(result).toISOString().slice(0, 10)); //setting departureDate.
  }

  return (
  <form>
    <div>
      <label>Booker's Name</label>
      <div>
      <input type="text" value={bookersName} onChange={(e)=>setBookersName(e.target.value)}/>
      </div>
    </div>
    <div>
      <label>Email</label>
      <div>
      <input type="email" value={email} onChange={(e)=>setEmail(e.target.value)}/>
      </div>
    </div>
    <div>
      <label>Country</label>
      <div>
      <input type="text" value={country} onChange={(e)=>setCountry(e.target.value)}/>
      </div>
    </div>
    <div>
      <label>Adults</label>
      <div>
      <input type="number" value={adults} onChange={(e)=>setAdults(e.target.value)}/>
      </div>
    </div>
    <div>
      <label>Children</label>
      <div>
      <input type="number" value={children} onChange={(e)=>setChildren(e.target.value)}/>
      </div>
    </div>
    <div>
      <label>Infants</label>
      <div>
      <input type="number" value={infants} onChange={(e)=>setInfants(e.target.value)}/>
      </div>
    </div>
    <div>
      <label>Arrival's Date</label>
      <div>
      <input type="date" min={baseDate} value={arrivalDate}  onChange={(e)=>{
        setArrivalDate(e.target.value)
        getTomorrowsDate(e); // sets departureDate to 1 day later
      }}/>
      </div>
    </div>
    <div>
      <label>Departure's Date</label>
      <div>
      <input type="date" min={normalDate} value={departureDate}  onChange={(e)=>setDepartureDate(e.target.value)}/>
      </div>
    </div>
    <div>
      <input type="submit" onClick={onclick}/>
    </div>
  </form>
  )
}

export default BookingForm
