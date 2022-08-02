import {Link, useParams} from 'react-router-dom';
import { useState,useEffect } from 'react';
import { getApi } from '../Clients/requests';

const BookingDetails = () => {
    const params = useParams();

    const url=params.bookingId;
    const [booking,setBooking]=useState({});
    
    useEffect(()=>{
    getApi(url).then(data=>{
    setBooking(data);
            });
        }
      ,[url]);

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
        </div>
      )
}

export default BookingDetails
