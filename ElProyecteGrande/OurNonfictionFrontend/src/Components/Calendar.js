import React from 'react'
import { useState, useEffect } from "react";
import {
  format,
  subMonths,
  addMonths,
  startOfWeek,
  addDays,
  isSameDay,
  lastDayOfWeek,
  addWeeks,
  subWeeks
} from "date-fns";
import { getApi } from '../Clients/requests';

const Calendar = () => {
  const [currentMonth, setCurrentMonth] = useState(new Date());

  const changeMonthHandle = (btnType) => {
    if (btnType === "prev") {
      setCurrentMonth(subMonths(currentMonth, 1));
    }
    if (btnType === "next") {
      setCurrentMonth(addMonths(currentMonth, 1));
    }
  };

  const changeWeekHandle = (btnType) => {
    if (btnType === "prev") {
      setCurrentMonth(subWeeks(currentMonth, 1));
    }
    if (btnType === "next") {
      setCurrentMonth(addWeeks(currentMonth, 1));
    }
  };

  const renderHeader = () => {
    const dateFormat = "MMM yyyy";
    return (
      <>
      <div className="header row flex-middle">
        <div className="col">
          <div className="icon" onClick={() => changeMonthHandle("prev")}>
            prev month
          </div>
        </div>
        <div className="col">
        <div className="icon" onClick={() => changeWeekHandle("prev")}>
            prev week
          </div>
        </div>
        <div className="col">
          <span>{format(currentMonth, dateFormat)}</span>
        </div>
        <div className="col" onClick={() => changeWeekHandle("next")}>
          <div className="icon">next week</div>
        </div>
        <div className="col">
          <div className="icon" onClick={() => changeMonthHandle("next")}>next month</div>
        </div>
      </div>
      </>
    );
  };
  const renderDays = () => {
    const dateFormat = "EEE";
    const days = [];
    let startDate = startOfWeek(currentMonth, { weekStartsOn: 1 });
    for (let i = 0; i < 7; i++) {
      days.push(
        <div className="col col-center" key={i}>
          {format(addDays(startDate, i), dateFormat)}
        </div>
      );
    }
    return <div className="days row">{days}</div>;
  };
  const renderCells = () => {
    const startDate = startOfWeek(currentMonth, { weekStartsOn: 1 });
    const endDate = lastDayOfWeek(currentMonth, { weekStartsOn: 1 });
    const dateFormat = "d";
    const rows = [];
    let days = [];
    let day = startDate;
    let formattedDate = "";
    while (day <= endDate) {
      for (let i = 0; i < 7; i++) {
        formattedDate = format(day, dateFormat);

        days.push(
          <div
            className={`col ${
              isSameDay(day, new Date())
                ? "today"
                : ""
            }`}
            key={day}
          >
            <span className="number">{formattedDate}</span>
          </div>
        );
        day = addDays(day, 1);
      }

      rows.push(
        <div className="row" key={day}>
          {days}
        </div>
      );
      days = [];
    }
    return <div className="body">{rows}</div>;
  };

  const RenderRooms = () => {
    const [rooms, setRooms] = useState([]);
    const [bookings, setBookings] = useState([]);

    useEffect(() => {
      getApi('roomapi').then(data => {
        setRooms(data);
      });
      getApi('bookingapi').then(data => {
        setBookings(data);
      })
    }, []);

    const RenderBookings = (roomId) => {
      const startDate = startOfWeek(currentMonth, { weekStartsOn: 1 });
      const endDate = lastDayOfWeek(currentMonth, { weekStartsOn: 1 });
      const dateFormat = "yyyy-MM-dd";
      const rows = [];
      let days = [];
      let day = startDate;
      const dayCount = 7;
      while (day <= endDate) {
        for (let i = 0; i < dayCount; i++) {
          const formattedDate = format(day, dateFormat);
          const booking = bookings.filter((booking) => (new Date(formattedDate) >= new Date(booking.arrivalDate.slice(0, 10)) && new Date(formattedDate) <= new Date(booking.departureDate.slice(0, 10)) && booking.room.id === roomId))[0]

          let spanSize = 0;
          let style = '';
          if (booking) {
            if (formattedDate === booking.arrivalDate.slice(0, 10)) {
              spanSize = booking.nights + i > dayCount ? dayCount - i : booking.nights;
            } else {
              let end = new Date(booking.departureDate.slice(0, 10));
              if (end.getDay() - 1 < 1 && format(end, dateFormat) === formattedDate) {
                day = addDays(day, 1 + spanSize);
                continue;
              }
              spanSize = end >= new Date(format(addDays(day, dayCount - i), dateFormat)) ? dayCount : end.getDay() - 1;
            }
            style = {'gridColumnEnd':'span '+ spanSize, 'border': '2px solid red', 'gridColumnStart': i + 1}
            i += spanSize;
            days.push(
              <div
                className={`col${
                  isSameDay(day, new Date())
                    ? " today"
                    : ""}`}
                key={booking.id}
                style={style}
              >
                {booking.id}
              </div>
            );
          }
          day = addDays(day, 1 + spanSize);
        }
  
        rows.push(
          <div className="room-row" key={day}>
            {days}
          </div>
        );
        days = [];
      }
      return <>{rows}</>;
    };

    return (
      <>
      {rooms.map(room => 
        <div key={room.id} className='row'>
        <div className='col-2'>
          {room.id}
        </div>
        <div className='col-10'>
          {RenderBookings(room.id)}
        </div>
        </div>
      )}
      </>
    );
  }

  return (
    <>
    <div className='row'>
      {renderHeader()}
    </div>
    <div className='row'>
      <div className='col-2'>

      </div>
      <div className="col-10">
        {renderDays()}
        {renderCells()}
      </div>
    </div>
    {RenderRooms()}
    </>
  );
};

export default Calendar
