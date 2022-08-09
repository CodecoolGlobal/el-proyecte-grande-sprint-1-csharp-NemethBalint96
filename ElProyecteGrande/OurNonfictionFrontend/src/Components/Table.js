import React from 'react'
import { useNavigate } from 'react-router-dom';
const BookingTable = ({data,type}) => {

    const bookinTableHeader=["Id","Booker's Name","Email","Adults","Children","Infants","Total","Country",
"Arrival Date","Deaprture Date","Nights","Status","Created","Modification Date"];
    const guestTableHeader =["Id","Full Name","Birthplace","Birthdate","Email","Phone Number","Country","City",
"Address","Postal Code","Citizenship","Age"];
    
function configurePage(data, type) {
  if(type==='Booking'){
  data.map(booking => delete booking["guests"]);
  data.map(booking => delete booking["room"]);
  data.map(booking => booking["status"]= booking['status']!==0?"Cancelled":'Confirmed');
  data.map(booking =>booking['arrivalDate']= booking['arrivalDate'].slice(0,10));
  data.map(booking =>booking['departureDate']= booking['departureDate'].slice(0,10));
  data.map(booking =>booking['created']= booking['created'].slice(0,10));
  data.map(booking =>booking['modificationDate']= booking['modificationDate'].slice(0,10));
  }
  if (type !== 'Booking') {
    data.map(guest => guest["age"] = guest["age"] === 0 ? "Adult" : 1 ? "Child" : 2 ? "Infant" : "");
    data.map(guest =>guest['birthDate']=guest['birthDate'].slice(0,10));
  }
}


    configurePage(data, type);
    const navigate = useNavigate();

    function redirect(bookingId) {
        navigate(`/booking/${bookingId}`);
      }


    function CreateTableHead(){
        if(type!=='Booking'){
            return <thead className="text-center align-middle"><tr>{guestTableHeader.map(head=><th>{head}</th>)}</tr></thead>
        }
        return <thead className="text-center align-middle"><tr>{bookinTableHeader.map(head=><th>{head}</th>)}</tr></thead>
    }

    function CreateTable(){
        if(type!=='Booking'){
            return  data.map((item) => (
                <tbody>
                    <tr className="text-center align-middle" key={item.id}>
                      {Object.values(item).map((val) => (
                        <td>{val}</td>
                      ))}
                    </tr>
                    </tbody>
                  ))} 
        
        return  data.map((item) => (
            <tbody>
                <tr onClick={() => redirect(item.id)} className="text-center align-middle" key={item.id}>
                  {Object.values(item).map((val) => (
                    <td>{val}</td>
                  ))}
                </tr>
                </tbody>
              ))}
        
return (
    <>
   {  data.length!==0?<table className="table table-sm table-responsive table-striped table-success table-hover align-middle" >
      {CreateTableHead()}
     {CreateTable()}
    </table>:""}
    
    </>
  )
}

export default BookingTable


