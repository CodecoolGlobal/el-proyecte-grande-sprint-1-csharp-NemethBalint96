import { useState } from 'react';
import './index.css';
import App from './App';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import GuestTable from './Components/GuestTable';
import BookingDetails from './Components/BookingDetails';
import MainPage from './Components/MainPage';
import BookingForm from './Components/BookingForm';
import SelectRoom from './Components/SelectRoom';
import GuestForm from './Components/GuestForm';
import Calendar from './Components/Calendar';
import UserForm from './Components/UserForm';

function Main() {
  const [username, setUsername] = useState('');

  return (
    <BrowserRouter>
    <Routes>
    <Route path='/' forceRefresh={true} element={<App username={username} setUsername={setUsername}/>}>
    <Route path='/bookings' element={<MainPage/>}/>
    <Route path='/guests' element={<GuestTable/>}/>
    <Route path='/booking/:bookingId' element={<BookingDetails/>}/>
    <Route path='/newbooking' element={<BookingForm/>}/>
    <Route path='/available/:bookingId' element={<SelectRoom/>}/>
    <Route path='/guest/:guestId' element={<GuestForm/>}/>
    <Route path='/editbooking/:bookingId' element={<BookingForm />}/>
    <Route path='/' element={<Calendar/>}/>
    <Route path='/registration' element={<UserForm type="registration" setName={setUsername}/>}/>
    <Route path='/login' element={<UserForm type="login" setName={setUsername}/>}/>
  </Route>
  </Routes>
  </BrowserRouter>
  )
}

export default Main
