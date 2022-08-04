import { useState, useEffect } from "react";
import { getApi } from "../Clients/requests";
import { Link, useNavigate } from "react-router-dom";

const BookingTable = () => {
  const url = "booking";
  const [bookings, setBookings] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getApi(url).then(data=>{
      console.log(data);
      setBookings(data);
    });
  }
  ,[url]);

  function redirect(bookingId) {
    navigate(`/booking/${bookingId}`);
  }

  return (
    <>
    <div>
      <Link to="/newbooking"><button className="btn btn-primary">Add New Booking</button></Link>
    </div>
    <br></br>
    <table className="table table-sm table-responsive table-striped table-success table-hover align-middle">
      <thead className="text-center align-middle">
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
        {bookings.map(booking =>
          <tr className="text-center align-middle" key={booking.id} onClick={() => redirect(booking.id)} >
            <td>{booking.id}</td>
            <td>{booking.bookersName}</td>
            <td>{booking.email}</td>
            <td>{booking.nights}</td>
            <td>{booking.adults}</td>
            <td>{booking.children}</td>
            <td>{booking.infants}</td>
            <td>{booking.total}</td>
            <td>{booking.country}</td>
            <td>{booking.arrivalDate.slice(0, 10)}</td>
            <td>{booking.departureDate.slice(0, 10)}</td>
            <td>{booking.status === 0 ? "Confirmed" : "Cancelled"}</td>
            <td>{booking.created.slice(0, 10)}</td>
            <td>{booking.modificationDate.slice(0, 10)}</td>
          </tr>
        )}
      </tbody>
    </table>
    </>
  );
}

export default BookingTable;
