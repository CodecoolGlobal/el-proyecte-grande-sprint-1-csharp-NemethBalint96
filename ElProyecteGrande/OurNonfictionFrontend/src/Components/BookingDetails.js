import {Link, useParams} from 'react-router-dom';
import { useState,useEffect } from 'react';
import { deleteApi, getApi, postApi } from '../Clients/requests';

const BookingDetails = () => {
    const params = useParams();
    const url=params.bookingId;
    const [booking,setBooking]=useState({});
    const [guests,setGuests]=useState([]);
    const [newGuestAge,setNewGuestAge]=useState(0);
    
    useEffect(()=>{
    getApi(url).then(data=>{
    setBooking(data);
    setGuests(data.guests);
            });
        }
      ,[url]);

  function OnClick(){
    getApi(url).then(data=>{
    setBooking(data);
    setGuests(data.guests);
            });
          }



  const body={
    'age':parseInt(newGuestAge)
  }

  function onClick(){
    postApi(`/booking/${params.bookingId}/addnew`,body).then(getApi(url).then(data=>{
      setBooking(data);
      setGuests(data.guests);
      const element = document.getElementById('select');
      element.style.display='none';
      const button = document.getElementById('button');
      button.style.display='';
    }))
  }
     
return (
<div>
  <div>
    <Link to={`/available/${booking.id}`}>Add room to Booking</Link>
    <Link to={`/editbooking/${booking.id}`}>Edit booking</Link>
  </div>
        <table>
            <thead>
              <tr>
                <th>Id</th>
                <th>Booker's Name</th>
                <th>Email</th>
                <th>Nights</th>
                <th>Adults</th>
                <th>Children</th>
                <th>Infants</th>
                <th>Total</th>
                <th>Country</th>
                <th>Arrival</th>
                <th>Departure</th>
                <th>Status</th>
                <th>Created</th>
                <th>Modified</th>
              </tr>
            </thead>
            <tbody>
          <tr key={booking.id}>
          <td>{booking.id}</td>
          <td>{booking.bookersName}</td>
          <td>{booking.email}</td>
          <td>{booking.nights}</td>
          <td>{booking.adults}</td>
          <td>{booking.children}</td>
          <td>{booking.infants}</td>
          <td>{booking.total}</td>
          <td>{booking.country}</td>
          <td>{booking.arrivalDate===undefined?"":booking.arrivalDate.slice(0,10)}</td>
          <td>{booking.departureDate===undefined?"":booking.departureDate.slice(0,10)}</td>
          <td>{booking.status===0?"Confirmed":"Cancelled"}</td>
          <td>{booking.created===undefined?"":booking.created.slice(0,10)}</td>
          <td>{booking.modificationDate===undefined?"":booking.modificationDate.slice(0,10)}</td>
          </tr>
          </tbody>
        </table>
        <br/><br/>
        <div>
      <div>
        <button id="button" onClick={()=>{
          const element= document.getElementById('select');
          element.style.display='';
          const button=document.getElementById('button');
          button.style.display='none';
          
        }}>New Guest</button>
      </div>
      <div id='select' style={{display:'none'}}>
            <select  onChange={(e)=>setNewGuestAge(e.target.value)}>
            <option value="0" >Adult</option>
            <option value ='1'>Child</option>
            <option value="2">Infant</option>
          </select>
         <input type="submit" onClick={onClick}/>
         </div>
    </div>
        <div>
        <table>
            <thead>
                <th>Id</th>
                <th>Full Name</th>
                <th>Birth Date</th>
                <th>Birth Place</th>
                <th>Email</th>
                <th>Phone Number</th>
                <th>Country</th>
                <th>City</th>
                <th>Address</th>
                <th>Postal Code</th>
                <th>Citizenship</th>
                <th>Edit</th>
                <th>Delete</th>
            </thead>
            <tbody>
          {guests.map(guest=><tr key={guest.id}>
          <td>{guest.id}</td>
          <td>{guest.fullName}</td>
          <td>{guest.birthPlace}</td>
          <td>{guest.birthDate}</td>
          <td>{guest.email}</td>
          <td>{guest.phone}</td>
          <td>{guest.country}</td>
          <td>{guest.city}</td>
          <td>{guest.address}</td>
          <td>{guest.postalCode}</td>
          <td>{guest.citizenship}</td>
          <td><Link to={`/guest/${guest.id}`}><button>Edit</button></Link></td>
          <td><button onClick={()=>{
            deleteApi(`/guest/${guest.id}`).then(()=>{OnClick()})
            
            }} >Delete</button></td>
          </tr>)}
          </tbody>
        </table>
        </div>
        </div>
      )
}

export default BookingDetails
