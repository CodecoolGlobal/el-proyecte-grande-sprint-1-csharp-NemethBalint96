import { useEffect, useState } from "react";
import { useParams,useNavigate } from "react-router-dom";
import { getApi } from "../Clients/requests";

const SelectRoom = () => {
    const navigate = useNavigate();
    const urlGet= "/room";
    const params = useParams();
    const id = params.bookingId;
    const [rooms,setRooms]=useState([]);
    const[roomId,setRoomId]=useState({});
    const url =`/room/available/${id}`;
    useEffect(()=>{
        getApi(url).then(data=>{
        setRooms(data);
        setRoomId(data[0].id);
                });
            }
          ,[url]);

function onClick(e){
e.preventDefault();
getApi(urlGet+"/"+roomId+"/"+id);
navigate(`/booking/${id}`);

}

  return (
    <div className="container form-control">
      <label className="form-label">Available Room</label>
      <select className="form-select" onChange={(e)=>setRoomId(e.target.value)}>
        {rooms.map(room=><option value={room.id} key={room.id}>Floor Number: {room.floor} Room Number: {room.doorNumber} Room Type: {room.roomType===1?"Apartman":room.roomType===2?"Standard":room.roomType===3?"Superior":""}</option>)}
      </select>
      <div>
      <br></br>
      <input className="form-control btn btn-primary" type="submit" onClick={onClick}/>
      </div>
    </div>
  )
}

export default SelectRoom
