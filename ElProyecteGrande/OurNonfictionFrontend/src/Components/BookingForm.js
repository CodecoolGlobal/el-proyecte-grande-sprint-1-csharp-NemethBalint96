import { useState, useEffect } from "react";
import { getApi, postApi, putApi } from "../Clients/requests";
import { useNavigate, useParams } from "react-router-dom";

let now = new Date();
let baseDate = now.toISOString().slice(0, 10);
let tomorrow = now.setDate(now.getDate() + 1);
let sec = tomorrow;
let normalDate = new Date(sec).toISOString().slice(0, 10);

const BookingForm = () => {
  const navigate = useNavigate();
  const [bookersName, setBookersName] = useState('');
  const [email, setEmail] = useState('');
  const [country, setCountry] = useState('');
  const [adults, setAdults] = useState(0);
  const [children, setChildren] = useState(0);
  const [infants, setInfants] = useState(0);
  const [arrivalDate, setArrivalDate] = useState(baseDate);
  const [departureDate, setDepartureDate] = useState(normalDate);
  const [guests, setGuests] = useState([]);

  const params = useParams();
  const bookingId = params.bookingId > 0 ? params.bookingId : null;
  const body = {
    "bookersName":bookersName,
    "email":email,
    "country":country,
    "adults":adults,
    "children":children,
    "infants":infants,
    "arrivalDate":arrivalDate,
    "departureDate": departureDate,
    "guests":guests,
  }

  useEffect(() => {
    if(bookingId) {
      getApi(`/bookingapi/${bookingId}`).then(data => {
        setBookersName(data.bookersName);
        setEmail(data.email);
        setCountry(data.country);
        setAdults(data.adults);
        setChildren(data.children);
        setInfants(data.infants);
        setArrivalDate(data.arrivalDate.slice(0, 10));
        setDepartureDate(data.departureDate.slice(0, 10));
        setGuests(data.guests);
      })
    }
  }, [bookingId])

  const onclick = (e) => {
    e.preventDefault();
    if(!bookingId) {
      postApi("bookingapi", body).then(data => {
        navigate(`/available/${data.id}`);
      })
    } else {
        //body.bookingId = parseInt(bookingId);
        console.log(body);
      putApi(`/bookingapi/${bookingId}`, body).then((response) => {
        if(response.status === 200) {
          navigate(`/booking/${bookingId}`)
        }
      });
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
    <div className="container form-control">
      <form>
        <div className="row">
          <div className="col">
            <label className="form-label">Booker's Name</label><br/>
            <input className="form-control" type="text" value={bookersName} onChange={(e)=>setBookersName(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Email</label><br/>
            <input className="form-control" type="email" value={email} onChange={(e)=>setEmail(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Country</label><br/>
            <input className="form-control" type="text" value={country} onChange={(e)=>setCountry(e.target.value)}/>
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="col">
            <label className="form-label">Adults</label><br/>
            <input className="form-control" type="number" value={adults} onChange={(e)=>setAdults(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Children</label><br/>
            <input className="form-control" type="number" value={children} onChange={(e)=>setChildren(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Infants</label><br/>
            <input className="form-control" type="number" value={infants} onChange={(e)=>setInfants(e.target.value)}/>
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="col">
            <label className="form-label">Arrival's Date</label><br/>
            <input className="form-control" type="date" min={baseDate} value={arrivalDate} onChange={(e)=>{
              setArrivalDate(e.target.value)
              getTomorrowsDate(e); // sets departureDate to 1 day later
            }}/>
          </div>
          <div className="col">
            <label className="form-label">Departure's Date</label><br/>
            <input className="form-control" type="date" min={normalDate} value={departureDate} onChange={(e)=>setDepartureDate(e.target.value)}/>
          </div>
        </div>
        <br></br>
        <br></br>
        <div>
          <input className="form-control btn btn-primary" type="submit" onClick={onclick}/>
        </div>
      </form>
    </div>
  );
}

export default BookingForm;
