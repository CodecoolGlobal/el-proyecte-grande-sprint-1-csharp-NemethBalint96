import { Link, useParams} from 'react-router-dom';
import { useState, useEffect } from 'react';
import { deleteApi, getApi, postApi } from '../Clients/requests';

const BookingDetails = () => {
  const params = useParams();
  const url = params.bookingId;
  const [booking, setBooking] = useState({});
  const [guests, setGuests] = useState([]);
  const [room, setRoom] = useState({});
  const [newGuestAge, setNewGuestAge] = useState(0);

  useEffect(() => {
    getApi(url).then(data => {
      setBooking(data);
      setGuests(data.guests);
      setRoom(data.room);
    });
  } ,[url]);

  function OnClick() {
    getApi(url).then(data => {
      setBooking(data);
      setGuests(data.guests);
      setRoom(data.room);
    });
  }

  const body = {
    'age':parseInt(newGuestAge)
  }

  function onClick() {
    const body = {
      "fullName":'Accompanying Guest',
      "birthPlace":'',
      "birthDate":'0001-01-01',
      "email":'',
      "phone":'',
      "country":'',
      "city":'',
      "address":'',
      "postalCode":0,
      "citizenship":'',
      "age":parseInt(newGuestAge),
      "comment":'',
    }
    console.log(body)
    postApi(`/booking/${params.bookingId}/addnew`, body).then(() => {getApi(url).then(data => {
      setBooking(data);
      setGuests(data.guests);
      const element = document.getElementById('select');
      element.style.display='none';
      const button = document.getElementById('button');
      button.style.display='';
    })})
  }

  function cancelBooking() {
    deleteApi(`/booking/${url}`).then(() => {
      OnClick();
    })
  }

  return (
    <>
      <h1 className='display-5'>Reservation</h1>
      <div className='row'>
        <div className='col'>
          <button className="btn btn-primary btn-danger" onClick={cancelBooking}>Cancel</button>
        </div>
        <div className='col'>
          <Link to={`/editbooking/${booking.id}`}><button className="btn btn-primary">Edit</button></Link>
        </div>
        <div className='col'>
          <Link to={`/available/${booking.id}`}><button className="btn btn-primary">Select Room</button></Link>
        </div>
      </div>
      <div className="row">
        <div className="card col">
          <ul className="list-group list-group-flush">
            <li className="list-group-item">Reservation Number: {booking.id}</li>
            <li className="list-group-item">Status: {booking.status === 0 ? 'Confirmed' : 'Cancelled'}</li>
            <li className="list-group-item">Night(s): {booking.nights}</li>
            <li className="list-group-item">Number of Guest(s): {booking.adults + booking.children + booking.infants}</li>
            <li className="list-group-item">Adult(s): {booking.adults}</li>
            <li className="list-group-item">Child(ren): {booking.children}</li>
            <li className="list-group-item">Infant(s): {booking.infants}</li>
          </ul>
        </div>
        <div className="card col">
          <ul className="list-group list-group-flush">
            <li className="list-group-item">Booker's Name: {booking.bookersName}</li>
            <li className="list-group-item">Booker's Email: {booking.email}</li>
            <li className="list-group-item">Country: {booking.country}</li>
            <li className="list-group-item">Arrival Date: {booking.arrivalDate && booking.arrivalDate.slice(0, 10)}</li>
            <li className="list-group-item">Departure Date: {booking.departureDate && booking.departureDate.slice(0, 10)}</li>
            <li className="list-group-item">Created: {booking.created && booking.created.slice(0, 10)}</li>
            <li className="list-group-item">Modification: {booking.modificationDate && booking.modificationDate.slice(0, 10)}</li>
          </ul>
        </div>
        <div className="card col">
          <div className="card-header">Room details</div>
          <ul className="list-group list-group-flush">
            <li className="list-group-item">Room Type: {room.roomType === 1 ? 'Apartman' : room.roomType === 3 ? 'Superior' : 'Standard'}</li>
            <li className="list-group-item">Floor: {room.floor}</li>
            <li className="list-group-item">DoorNumber: {room.doorNumber}</li>
            <li className="list-group-item">Comment: {room.comment}</li>
            <li className="list-group-item">Room Price: {room.price}$</li>
            <li className="list-group-item">Total Price: {room.price * booking.nights}$</li>
          </ul>
        </div>
      </div>
      <h1 className="display-5">Guests</h1>
      <div>
        <div id="button">
          <button className="btn btn-primary" onClick={()=>{
            const element= document.getElementById('select');
            element.style.display='';
            const button=document.getElementById('button');
            button.style.display='none';
          }}>New Guest</button>
        </div>
        <div className='row' id='select' style={{display:'none'}}>
          <div className='col-md-2'>
            <select className='form-select' onChange={(e)=>{setNewGuestAge(e.target.value)}}>
              <option value="0">Adult</option>
              <option value="1">Child</option>
              <option value="2">Infant</option>
            </select>
          </div>
          <div className='col'>
            <input className="btn btn-primary" type="submit" value='Add' onClick={onClick}/>
          </div>
        </div>
      </div>
      <div>
        <table className="table table-sm table-responsive table-striped table-success table-hover align-middle table-bordered border-primary">
          <thead className="text-center align-middle">
            <tr>
              <th>Id</th>
              <th>Full Name</th>
              <th>Age</th>
              <th>Birth Place</th>
              <th>Birth Date</th>
              <th>Email</th>
              <th>Phone Number</th>
              <th>Country</th>
              <th>City</th>
              <th>Address</th>
              <th>Postal Code</th>
              <th>Citizenship</th>
              <th>Edit</th>
              <th>Delete</th>
            </tr>
          </thead>
          <tbody>
          {guests.map(guest=><tr className="text-center align-middle" key={guest.id}>
          <td>{guest.id}</td>
              <td>{guest.fullName}</td>
                <td>{guest.age===0?"Adult":guest.age===1?"Child":guest.age===2?"Infant":""}</td>
          <td>{guest.birthPlace}</td>
          <td>{guest.birthDate && guest.birthDate.slice(0, 10)}</td>
          <td>{guest.email}</td>
          <td>{guest.phone}</td>
          <td>{guest.country}</td>
          <td>{guest.city}</td>
          <td>{guest.address}</td>
          <td>{guest.postalCode}</td>
          <td>{guest.citizenship}</td>
          <td><Link to={`/guest/${guest.id}`}><button className="btn btn-outline-secondary">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pen" viewBox="0 0 16 16">
              <path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001zm-.644.766a.5.5 0 0 0-.707 0L1.95 11.756l-.764 3.057 3.057-.764L14.44 3.854a.5.5 0 0 0 0-.708l-1.585-1.585z"></path>
            </svg>
            </button></Link></td>
          <td><button className="btn btn-outline-secondary" onClick={() => {
            deleteApi(`/guest/${guest.id}`).then(()=>{OnClick()})
            }}>
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
              <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
              <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
            </svg>
            </button>
          </td>
          </tr>)}
          </tbody>
        </table>
        </div>
    </>
  );
}

export default BookingDetails;
