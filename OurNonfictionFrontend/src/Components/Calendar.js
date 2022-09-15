import React from 'react'
import { useState, useEffect } from 'react'
import {
  format,
  subMonths,
  addMonths,
  startOfWeek,
  addDays,
  isSameDay,
  lastDayOfWeek,
  addWeeks,
  subWeeks,
  isSameMonth
} from 'date-fns'
import { useNavigate } from 'react-router-dom'
import { getApi } from '../Clients/requests'

const Calendar = () => {
  const [currentMonth, setCurrentMonth] = useState(new Date())
  const [loading, setLoading] = useState(false)
  const navigate = useNavigate()
  const [rooms, setRooms] = useState([])
  const [bookings, setBookings] = useState([])

  useEffect(() => {
    setLoading(true)
    getApi('roomapi')
      .then((data) => setRooms(data))
      .catch(() => navigate('/error'))
    getApi('bookingapi')
      .then((data) => {
        setBookings(data)
      })
      .then(() => setLoading(false))
      .catch(() => {
        navigate('/error')
      })
  }, [])

  const changeMonthHandle = (btnType) => {
    if (btnType === 'prev') {
      setCurrentMonth(subMonths(currentMonth, 1))
    }
    if (btnType === 'next') {
      setCurrentMonth(addMonths(currentMonth, 1))
    }
  }

  const changeWeekHandle = (btnType) => {
    if (btnType === 'prev') {
      setCurrentMonth(subWeeks(currentMonth, 1))
    }
    if (btnType === 'next') {
      setCurrentMonth(addWeeks(currentMonth, 1))
    }
  }

  const renderHeader = () => {
    const dateFormat = 'MMM yyyy'
    return (
      <>
        <div className="row">
          <div className="col">
            <div className="btn btn-primary" onClick={() => changeMonthHandle('prev')}>
              Prev month
            </div>
          </div>
          <div className="col">
            <div className="btn btn-primary" onClick={() => changeWeekHandle('prev')}>
              Prev week
            </div>
          </div>
          <div
            className={`col text-center align-middle ${
              isSameMonth(currentMonth, new Date()) ? 'bg-warning' : ''
            }`}>
            {format(currentMonth, dateFormat)}
          </div>
          <div className="col" onClick={() => changeWeekHandle('next')}>
            <div className="btn btn-primary">Next week</div>
          </div>
          <div className="col">
            <div className="btn btn-primary" onClick={() => changeMonthHandle('next')}>
              Next month
            </div>
          </div>
        </div>
      </>
    )
  }

  const renderDays = () => {
    const dateFormat = 'EEE'
    const days = [
      <th key={-1} className="col-2" rowSpan="2">
        Rooms
      </th>
    ]
    let startDate = startOfWeek(currentMonth, { weekStartsOn: 1 })
    for (let i = 0; i < 7; i++) {
      days.push(
        <th className="col" key={i}>
          {format(addDays(startDate, i), dateFormat)}
        </th>
      )
    }
    return <tr className="text-center align-middle">{days}</tr>
  }

  const renderCells = () => {
    const startDate = startOfWeek(currentMonth, { weekStartsOn: 1 })
    const endDate = lastDayOfWeek(currentMonth, { weekStartsOn: 1 })
    const dateFormat = 'd'
    const week = []
    let days = []
    let day = startDate
    let formattedDate = ''
    while (day <= endDate) {
      for (let i = 0; i < 7; i++) {
        formattedDate = format(day, dateFormat)

        days.push(
          <th
            className={` ${isSameDay(day, new Date()) ? 'bg-warning' : ''}`}
            scope="col"
            key={day}>
            {formattedDate}
          </th>
        )
        day = addDays(day, 1)
      }

      week.push(
        <tr className="" key={day}>
          {days}
        </tr>
      )
      days = []
    }
    return <>{week}</>
  }

  const RenderRooms = () => {
    const RenderBookings = (roomId) => {
      const startDate = startOfWeek(currentMonth, { weekStartsOn: 1 })
      const endDate = lastDayOfWeek(currentMonth, { weekStartsOn: 1 })
      const dateFormat = 'yyyy-MM-dd'
      const rows = []
      let days = []
      let day = startDate
      const dayCount = 7
      while (day <= endDate) {
        for (let i = 0; i < dayCount; i++) {
          const formattedDate = format(day, dateFormat)
          const booking = bookings.filter(
            (booking) =>
              new Date(formattedDate) >= new Date(booking.arrivalDate.slice(0, 10)) &&
              new Date(formattedDate) <= new Date(booking.departureDate.slice(0, 10)) &&
              booking.room.id === roomId
          )[0]

          let spanSize = 0
          if (booking && booking.status === 0) {
            if (formattedDate === booking.arrivalDate.slice(0, 10)) {
              spanSize = booking.nights + i > dayCount ? dayCount - i : booking.nights
            } else {
              let end = new Date(booking.departureDate.slice(0, 10))
              if (end.getDay() - 1 < 1 && format(end, dateFormat) === formattedDate) {
                day = addDays(day, 1 + spanSize)
                i -= 1
                continue
              }
              spanSize =
                end >= new Date(format(addDays(day, dayCount - i), dateFormat))
                  ? dayCount
                  : end.getDay() - 1
            }
            i += spanSize - 1
            days.push(
              <td
                className="bg-success text-white text-truncate table-hover"
                key={booking.id}
                colSpan={spanSize}
                style={{ cursor: 'pointer' }}
                onClick={() => {
                  navigate(`/booking/${booking.id}`)
                }}>
                {booking.id} {booking.bookersName}
              </td>
            )
          } else {
            days.push(<td key={day}></td>)
          }
          day = addDays(day, 1 + spanSize)
        }

        rows.push(<>{days}</>)
        days = []
      }
      return <>{rows}</>
    }

    return (
      <>
        {rooms.map((room) => (
          <tr key={room.id} className="">
            <th className="" scope="row">
              {room.roomType === 1 ? 'Apartman' : room.roomType === 3 ? 'Superior' : 'Standard'} Flr{' '}
              {room.floor} No. {room.doorNumber}
            </th>
            {RenderBookings(room.id)}
          </tr>
        ))}
      </>
    )
  }

  return (
    <>
      {loading ? (
        <div className="loader-container">
          <div className="spinner"></div>
        </div>
      ) : (
        <>
          {renderHeader()}
          <br></br>
          <table
            className="table table-sm table-striped  align-middle table-bordered table-border-2 border-2 border-primary text-center"
            style={{ tableLayout: 'fixed' }}>
            <thead>
              {renderDays()}
              {renderCells()}
            </thead>
            <tbody>{RenderRooms()}</tbody>
          </table>
        </>
      )}
    </>
  )
}

export default Calendar
