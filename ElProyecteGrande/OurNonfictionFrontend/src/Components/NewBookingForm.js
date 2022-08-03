import { useState } from "react"
import { postApi } from "../Clients/requests";
import {  useNavigate } from "react-router-dom";


let now = new Date();

let baseDate= now.toISOString().slice(0, 10);

let tomorrow = now.setDate(now.getDate()+1);
let sec = tomorrow;
let normalDate = new Date(sec).toISOString().slice(0,10);

const NewBookingForm = () => {
    const navigate=useNavigate();
    const[bookersName,setBookersName]=useState({});
    const[email,setEmail]=useState({});
    const[country,setCountry]=useState({});
    const[adults,setAdults]=useState({});
    const[children,setChildren]=useState({});
    const[infants,setInfants]=useState({});
    const[arrivalDate,setArrivalDate]=useState(baseDate);
    const[departureDate,setDepartureDate]=useState(normalDate);
    const body={
        "bookersName":bookersName,
        "email":email,
        "country":country,
        "adults":adults,
        "children":children,
        "infants":infants,
        "arrivalDate":arrivalDate,
        "departureDate":departureDate
    }

  const onclick=(e)=>{
    e.preventDefault();
    postApi("booking",body).then(data=>{
        navigate(`/available/${data.id}`);
    });
}


function getTomorrowsDate(e) {
    let targetDate = e.target.value;
    let date = new Date(targetDate); //converts IsoString to date object
    let ms = date.getTime(targetDate); // convert date to milliseconds
    let result = ms + 86400000; // add one day in milliseconds to date
    setDepartureDate(new Date(result).toISOString().slice(0, 10)); //setting departureDate.
}
console.log(normalDate);

  return (
    <form>
        <div>
            <label>Booker's Name</label>
            <div>
            <input type="text" onChange={(e)=>setBookersName(e.target.value)} />
            </div>
        </div>
        <div>
            <label>Email</label>
            <div>
            <input type="email" onChange={(e)=>setEmail(e.target.value)}/>
            </div>
        </div>
        <div>
            <label>Country</label>
            <div>
            <input type="text" onChange={(e)=>setCountry(e.target.value)}/>
            </div>
        </div>
        <div>
            <label>Adults</label>
            <div>
            <input type="number" onChange={(e)=>setAdults(e.target.value)}/>
            </div>
        </div>
        <div>
            <label>Children</label>
            <div>
            <input type="number" onChange={(e)=>setChildren(e.target.value)}/>
            </div>
        </div>
        <div>
            <label>Infants</label>
            <div>
            <input type="number" onChange={(e)=>setInfants(e.target.value)}/>
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

export default NewBookingForm


