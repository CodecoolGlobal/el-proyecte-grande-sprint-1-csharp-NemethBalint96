import { useState, useEffect } from "react";
import { getApi } from "../Clients/requests";

const GuestTable = () => {
  const url = 'guest';
  const [guests, setGuests] = useState([]);

  useEffect(()=>{
    getApi(url).then(data=>{
      setGuests(data);
    });
  },
  [url]);

  return (
    <table className="table table-sm table-responsive table-striped table-success table-hover align-middle">
      <thead className="text-center align-middle">
        <th>Id</th>
        <th>Full Name</th>
        <th>Birth Place</th>
        <th>Birth Date</th>
        <th>Email</th>
        <th>Phone Number</th>
        <th>Country</th>
        <th>City</th>
        <th>Address</th>
        <th>Postal Code</th>
        <th>Citizenship</th>
      </thead>
      <tbody>
        {guests.map(guest =>
          <tr className="text-center align-middle" key={guest.id}>
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
          </tr>
        )}
      </tbody>
    </table>
  );
}

export default GuestTable;
