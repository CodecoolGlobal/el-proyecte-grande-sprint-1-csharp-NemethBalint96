import { Link, useParams } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { deleteApi, getApi, postApi, putApi } from '../Clients/requests';
import BookingTable from './Table';

const BookingDetails = () => {
  const params = useParams();
  const url = `/bookingapi/${params.bookingId}`;
  const [booking, setBooking] = useState({});
  const [guests, setGuests] = useState([]);
  const [room, setRoom] = useState({});
  const [newGuestAge, setNewGuestAge] = useState(0);
  const [roomId, setRoomId] = useState(0);
  const [rooms, setRooms] = useState([]);
  const [loading,setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    getApi(`/roomapi/available/${params.bookingId}`).then(data => {
      setLoading(false);
      setRooms(data);
      setRoomId(data[0].id);
    });
    getApi(url).then(data => {
      setLoading(false);
      setBooking(data);
      setGuests(data.guests);
      setRoom(data.room);
    });
    
  } ,[url, params.bookingId]);

  function OnClick() {
    setLoading(true);
    getApi(url).then(data => {
      setLoading(false);
      setBooking(data);
      setGuests(data.guests);
      setRoom(data.room);
    });
  }

  function onClick() {
    setLoading(true);
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
    postApi(`/guestapi/${params.bookingId}/addnew`, body).then(() => {getApi(url).then(data => {
      setLoading(false);
      setBooking(data);
      setGuests(data.guests);
    })})
  }

  function cancelBooking() {
    deleteApi(`${url}`).then(() => {
      OnClick();
    })
  }

  function selectRoom() {
    const element = document.getElementById('roomSelect');
    element.style.display='';
    const button = document.getElementById('roomSelectButton');
    button.style.display='none';
  }

  function onSubmit(e) {
    setLoading(true);
    e.preventDefault();
      putApi(`/roomapi/${roomId}/${params.bookingId}`, null).then((response) => {
          if (response.status === 200) {
            setLoading(false);
              const newRoom = rooms.filter((room) => room.id === roomId);
              setRoom(newRoom[0]);
              getApi(`/roomapi/available/${params.bookingId}`).then(data => {
                setRooms(data);
                setRoomId(data[0].id);
              });
            const element = document.getElementById('roomSelect');
            element.style.display = 'none';
            const button = document.getElementById('roomSelectButton');
            button.style.display = '';
          }
      });
  }

  return (
    <>
    {loading?(<div className="loader-container">
    <div className="spinner"></div>
  </div>):(<>
    <h1 className='display-5'>Reservation</h1>
      <div className='row'>
        <div className='col'>
          {booking.status !== 1 ? <button className="btn btn-primary btn-danger" onClick={cancelBooking}>Cancel</button> : <></>}
        </div>
        <div className='col'>
        {booking.status !== 1 ? <Link to={`/editbooking/${booking.id}`}><button className="btn btn-primary">Edit</button></Link> : <></>}
        </div>
        <div className='col'>
          {booking.status !== 1 ? <>
            <button className="btn btn-primary" id='roomSelectButton' onClick={selectRoom}>Select Room</button>
            <div className='row' id='roomSelect' style={{display:'none'}}>
            <div className='col-md-7'>
                <select className='form-select' onChange={(e)=>setRoomId(parseInt(e.target.value))}>
                    {rooms.map(room => <option value={room.id} selected={room.id === roomId ? true : false } key={room.id}>Floor:{room.floor} Door:{room.doorNumber} {room.roomType===1?"Apartman":room.roomType===2?"Standard":room.roomType===3?"Superior":""}</option>)}
              </select>
              </div>
              <div className='col'>
              <input type="submit" className="btn btn-primary" onClick={e=>onSubmit(e)}/>
              </div>
            </div>
          </> : <></>}
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
            <li className="list-group-item">Modification: {booking.modificationDate && (booking.modificationDate.slice(0, 10) === '0001-01-01' ? '' : booking.modificationDate.slice(0, 10))}</li>
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
      {booking.status !== 1 ? <>
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
      </> : <></>}
      <div>
        {loading?<div className="loader-container">
    <div className="spinner"></div>
  </div>:
  <BookingTable data={booking} guests={guests} type='Guests' OnClick={OnClick}/>}
        </div>
    </>
  )}</>)}

export default BookingDetails;
