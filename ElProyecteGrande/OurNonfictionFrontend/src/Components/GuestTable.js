import { useState,useEffect } from "react"
import { getApi } from "../Clients/requests";

const GuestTable = () => {

const url='guest';
const [guests,setGuests]=useState([]);

useEffect(()=>{
    getApi(url).then(data=>{
        setGuests(data);
    });
}
  ,[url]);


    return (
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
          </tr>)}
          </tbody>
        </table>
      )
}

export default GuestTable
