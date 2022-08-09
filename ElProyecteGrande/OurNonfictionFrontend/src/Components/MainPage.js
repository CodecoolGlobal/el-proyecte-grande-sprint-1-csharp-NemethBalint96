import { useState, useEffect } from "react";
import { getApi } from "../Clients/requests";
import { Link} from "react-router-dom";
import BookingTable from "./BookingTable";


const MainPage = () => {
  const url = "bookingapi";
  const[firstBooking,setFirstBookings]=useState([]);
  const [bookings, setBookings] = useState([]);
  
  {console.log(bookings)}
  
  const handleSearch = (event) => {
    let search = event.target.value;
    const data = bookings.filter((item) =>
    item.bookersName.toLowerCase().includes(search.toLowerCase()))
    setBookings(data);
    if(search===""){
    setBookings(firstBooking);
    }
  };
  
  useEffect(() => {
    getApi(url).then(data=>{
      console.log(data);
      setFirstBookings(data);
      setBookings(data);

  })}
  ,[url]);

 

  return (
    <>
    <div>
      <Link to="/newbooking"><button className="btn btn-primary">Add New Booking</button></Link>
    </div>
    <br></br>
   <div className="row">
    <div className="col-md-2">
  <input  type="text" id="myInput" onChange={(e)=>{handleSearch(e)}} placeholder="Start typing a name..."/>
  </div>
  </div>
  <br></br>
    {bookings.length!==0?<BookingTable bookings={bookings}/>:<p></p>}
    </>
  );
}

export default MainPage;
