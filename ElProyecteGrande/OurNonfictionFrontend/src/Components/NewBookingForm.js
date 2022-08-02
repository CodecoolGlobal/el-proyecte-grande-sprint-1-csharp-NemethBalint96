import { useState } from "react"
import { postApi } from "../Clients/requests";
import {  useNavigate } from "react-router-dom";


const NewBookingForm = () => {
    const navigate=useNavigate();
    const[bookersName,setBookersName]=useState({});
    const[email,setEmail]=useState({});
    const[country,setCountry]=useState({});
    const[adults,setAdults]=useState({});
    const[children,setChildren]=useState({});
    const[infants,setInfants]=useState({});
    const[arrivalDate,setArrivalDate]=useState({});
    const[departureDate,setDepartureDate]=useState({});
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
            <input type="date" onChange={(e)=>setArrivalDate(e.target.value)}/>
            </div>
        </div>
        <div>
            <label>Departure's Date</label>
            <div>
            <input type="date" onChange={(e)=>setDepartureDate(e.target.value)}/>
            </div>
        </div>
        <div>
            <input type="submit" onClick={onclick}/>
        </div>
      
    </form>
  )
}

export default NewBookingForm
