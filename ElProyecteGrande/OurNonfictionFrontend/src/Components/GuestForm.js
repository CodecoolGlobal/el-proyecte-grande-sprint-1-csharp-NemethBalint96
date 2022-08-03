import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getApi, putApi } from "../Clients/requests";

const GuestForm = () => {
  const params = useParams();
  const [guest, setGuest] = useState({});
  const url = `/guest/${params.guestId}`;
  const [name, setName]=useState({});
  const [birthPlace, setBirthPlace] = useState("");
  const [birthDate, setBirthDate] = useState("0001-01-01");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [country, setCountry] = useState("");
  const [city, setCity] = useState("");
  const [address, setAddress] = useState("");
  const [postalCode, setPostalCode] = useState("");
  const [citizenship, setCitizenship] = useState("");
  const navigate = useNavigate();
  const body = {
    "id":parseInt(params.guestId),
    "fullName":name,
    "birthPlace":birthPlace === null ? "" : birthPlace,
    "birthDate":birthDate,
    "email":email,
    "phone":phone,
    "country":country,
    "city":city,
    "address":address,
    "postalCode":postalCode,
    "citizenship":citizenship,
    "comment":"",
  }

  useEffect(() => {
    getApi(url).then(data => {
      setGuest(data);
      setName(data.fullName);
      setBirthPlace(data.birthPlace);
      setBirthDate(data.birthDate.slice(0, 10));
      setEmail(data.email && "");
      setPhone(data.phone && "");
      setCountry(data.country && "");
      setCity(data.city && "");
      setAddress(data.address && "");
      setPostalCode(data.postalCode && 0);
      setCitizenship(data.citizenship && "");
    });
  }, [url]);
    
  const onclick = (e) => {
    e.preventDefault();
    putApi(`/guest/${guest.id}`, body).then(() =>
    {
      navigate(-1);
    });
  }
    
  return (
    <div>
      <label>Name</label>
      <div>
      <input type="text" value={name} onChange={(e)=>setName(e.target.value)}/>
      </div>
      <label>Birth Place</label>
      <div>
      <input type="text"  value={birthPlace && ""} onChange={(e)=>setBirthPlace(e.target.value)}/>
      </div>
      <label>Birth Date</label>
      <div>
      <input type="date" value={birthDate} onChange={(e)=>setBirthDate(e.target.value)}/>
      </div>
      <label>Email</label>
      <div>
      <input type="text" value={email && ""} onChange={(e)=>setEmail(e.target.value)}/>
      </div>
      <label>Phone</label>
      <div>
      <input type="text" value={phone && ""} onChange={(e)=>setPhone(e.target.value)}/>
      </div>
      <label>Country</label>
      <div>
      <input type="text" value={country && ""} onChange={(e)=>setCountry(e.target.value)}/>
      </div>
      <label>City</label>
      <div>
      <input type="text" value={city && ""} onChange={(e)=>setCity(e.target.value)}/>
      </div>
      <label>Address</label>
      <div>
      <input type="text" value={address && ""} onChange={(e)=>setAddress(e.target.value)}/>
      </div>
      <label>Postal Code</label>
      <div>
      <input type="number" value={postalCode && ""} onChange={(e)=>setPostalCode(e.target.value)}/>
      </div>
      <label>Citizenship</label>
      <div>
      <input type="text" value={citizenship && ""} onChange={(e)=>setCitizenship(e.target.value)}/>
      </div>
      <div>
        <input type="submit" onClick={onclick}/>
      </div>
    </div>
  );
}

export default GuestForm;
