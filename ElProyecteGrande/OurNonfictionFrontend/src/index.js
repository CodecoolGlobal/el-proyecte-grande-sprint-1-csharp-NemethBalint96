import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
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

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <BrowserRouter>
  <Routes>
    <Route path='/' element={<App/>}>
    <Route path='/bookings' element={<MainPage/>}/>
    <Route path='/guests' element={<GuestTable/>}/>
    <Route path='/booking/:bookingId' element={<BookingDetails/>}/>
    <Route path='/newbooking' element={<BookingForm/>}/>
    <Route path='/available/:bookingId' element={<SelectRoom/>}/>
    <Route path='/guest/:guestId' element={<GuestForm/>}/>
    <Route path='/editbooking/:bookingId' element={<BookingForm />}/>
    <Route path='/' element={<Calendar/>}/>
    <Route path='/registration' element={<UserForm />}/>
    <Route path='/login' element={<UserForm />}/>
  </Route>
  </Routes>
  </BrowserRouter>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
