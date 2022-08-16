import { useState, useEffect } from "react";
import { getApi } from "../Clients/requests";
import Table from "./Table";

const GuestTable = () => {
  const url = 'guestapi';
  const [guests, setGuests] = useState([]);

console.log(guests);

  useEffect(()=>{
    getApi(url).then(data=>{
      setGuests(data);
    });
  },
  [url]);

  return (
    <div>
        <Table data={guests} />
      </div>
  );
}

export default GuestTable;
