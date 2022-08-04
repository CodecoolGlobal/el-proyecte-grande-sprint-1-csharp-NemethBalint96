import { useEffect, useState } from "react";
import { useParams,useNavigate } from "react-router-dom";
import { getApi } from "../Clients/requests";

const SelectRoom = () => {
    const navigate = useNavigate();
    const urlGet= "/roomapi";
    const params = useParams();
    const id = params.bookingId;
    const [rooms,setRooms]=useState([]);
    const[roomId,setRoomId]=useState({});
    const url =`/roomapi/available/${id}`;
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
    <div>
      <select onChange={(e)=>setRoomId(e.target.value)}>
        {rooms.map(room=><option value={room.id} key={room.id}>{room.floor} {room.doorNumber} {room.roomType===1?"Apartman":room.roomType===2?"Standard":room.roomType===3?"Superior":""}</option>)}
      </select>
      <div>
      <input type="submit" onClick={onClick}/>
      </div>
    </div>
  )
}

export default SelectRoom
