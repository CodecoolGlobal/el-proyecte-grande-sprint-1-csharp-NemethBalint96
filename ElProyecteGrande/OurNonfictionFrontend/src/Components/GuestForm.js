import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getApi, putApi } from "../Clients/requests";

const GuestForm = () => {
  const params = useParams();
  const url = `/guestapi/${params.guestId}`;
  const [guest, setGuest] = useState({});
  const [name, setName] = useState("");
  const [birthPlace, setBirthPlace] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [country, setCountry] = useState("");
  const [city, setCity] = useState("");
  const [address, setAddress] = useState("");
  const [postalCode, setPostalCode] = useState(0);
  const [citizenship, setCitizenship] = useState("");
  const [age, setAge] = useState(0);
  const [comment, setComment] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
      getApi(url).then(data => {
          setGuest(data.result);
      setName(data.result.fullName);
        setBirthPlace(data.result.birthPlace === null ? '' : data.result.birthPlace);
        setBirthDate(data.result.birthDate.slice(0, 10) === '0001-01-01' ? "" : data.result.birthDate === undefined?"": data.result.birthDate.slice(0, 10));
      setEmail(data.result.email === null ? '' : data.result.email);
      setPhone(data.result.phone === null ? '' : data.result.phone);
      setCountry(data.result.country === null ? '' : data.result.country);
      setCity(data.result.city === null ? '' : data.result.city);
      setAddress(data.result.address === null ? '' : data.result.address);
      setPostalCode(data.result.postalCode);
      setCitizenship(data.result.citizenship === null ? '' : data.result.citizenship);
      setAge(data.result.age);
      setComment(data.result.comment === null ? '' : data.result.comment);
    });
  }, [url]);

  const onclick = (e) => {
    e.preventDefault();
    const body = {
      "id":parseInt(params.guestId),
      "fullName":name,
      "birthPlace":birthPlace,
      "birthDate":birthDate === '' ? '0001-01-01' : birthDate.slice(0, 10),
      "email":email,
      "phone":phone,
      "country":country,
      "city":city,
      "address":address,
      "postalCode":postalCode,
      "citizenship":citizenship,
      "age":parseInt(age),
      "comment":comment,
    }
    console.log(body)
    putApi(`/guestapi/${guest.id}`, body).then(() =>
    {
      navigate(-1);
    });
  }

  function select(e) {
    setAge(e.target.value);
  }

  return (
    <div className="container form-control">
      <form>
        <div className="row">
          <div className="col">
            <label className="form-label">Full Name</label><br/>
            <input className="form-control" type="text" value={name} onChange={(e)=>setName(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Birth Place</label><br/>
            <input className="form-control" type="text" value={birthPlace} onChange={(e)=>setBirthPlace(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Birth Date</label><br/>
            <input className="form-control" type="date" value={birthDate} onChange={(e)=>setBirthDate(e.target.value)}/>
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="col">
            <label className="form-label">Email</label><br/>
            <input className="form-control" type="text" value={email} onChange={(e)=>setEmail(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Phone</label><br/>
            <input className="form-control" type="text" value={phone} onChange={(e)=>setPhone(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Country</label><br/>
            <input className="form-control" type="text" value={country} onChange={(e)=>setCountry(e.target.value)}/>
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="col">
          <label className="form-label">City</label><br/>
          <input className="form-control" type="text" value={city} onChange={(e)=>setCity(e.target.value)}/>
          </div>
          <div className="col">
          <label className="form-label">Address</label><br/>
          <input className="form-control" type="text" value={address} onChange={(e)=>setAddress(e.target.value)}/>
          </div>
          <div className="col">
          <label className="form-label">Postal Code</label><br/>
          <input className="form-control" type="number" value={postalCode} onChange={(e)=>setPostalCode(e.target.value)}/>
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="col">
          <label className="form-label">Citizenship</label><br/>
          <input className="form-control" type="text" value={citizenship} onChange={(e)=>setCitizenship(e.target.value)}/>
          </div>
          <div className="col">
            <label className="form-label">Age</label><br/>
            <select className='form-select' onChange={(e)=>{select(e)}}>
              <option value={age}>Select</option>
              <option value="0">Adult</option>
              <option value="1">Child</option>
              <option value="2">Infant</option>
            </select>
          </div>
          <div className="col">
          <label className="form-label">Comment</label><br/>
          <input className="form-control" type="text" value={comment} onChange={(e)=>setComment(e.target.value)}/>
          </div>
        </div>
        <br></br>
        <br></br>
        <div>
          <input className="form-control btn btn-primary" type="submit" onClick={onclick}/>
        </div>
      </form>
    </div>
  );
}

export default GuestForm;
