import React from 'react'
import { useNavigate } from 'react-router-dom'
import { deleteApi } from '../Clients/requests'
import { Link } from 'react-router-dom'
const BookingTable = ({ data, type, OnClick, guests }) => {
  const bookinTableHeader = [
    'Id',
    "Booker's Name",
    'Email',
    'Adults',
    'Children',
    'Infants',
    'Country',
    'Arrival Date',
    'Deaprture Date',
    'Nights',
    'Revenue',
    'Status',
    'Created',
    'Modification Date'
  ]
  const guestTableHeader = [
    'Id',
    'Full Name',
    'Birthdate',
    'Birthplace',
    'Email',
    'Phone Number',
    'Country',
    'City',
    'Address',
    'Postal Code',
    'Citizenship',
    'Age of Guest'
  ]

  function configurePage(data, type) {
    if (type === 'Booking') {
      data.map((booking) => delete booking['guests'])
      data.map((booking) => delete booking['room'])
      data.map(
        (booking) =>
          (booking['status'] =
            booking['status'] === 0
              ? 'Confirmed'
              : booking['status'] === 'Confirmed'
              ? 'Confirmed'
              : 'Cancelled')
      )
      data.map((booking) => (booking['arrivalDate'] = booking['arrivalDate'].slice(0, 10)))
      data.map((booking) => (booking['departureDate'] = booking['departureDate'].slice(0, 10)))
      data.map((booking) => (booking['created'] = booking['created'].slice(0, 10)))
      data.map(
        (booking) => (booking['modificationDate'] = booking['modificationDate'].slice(0, 10))
      )
    }
    if (type === 'Guests') {
      guests.map(
        (guest) =>
          (guest['age'] =
            guest['age'] === 0
              ? 'Adult'
              : guest['age'] === 1
              ? 'Child'
              : guest['age'] === 'Adult'
              ? 'Adult'
              : guest['age'] === 'Child'
              ? 'Child'
              : 'Infant')
      )
      guests.map((guest) => (guest['birthDate'] = guest['birthDate'].slice(0, 10)))
      guests.map((guest) => delete guest['comment'])
    }
    if (type !== 'Booking' && type !== 'Guests') {
      data.map(
        (guest) =>
          (guest['age'] =
            guest['age'] === 0
              ? 'Adult'
              : guest['age'] === 1
              ? 'Child'
              : guest['age'] === 'Adult'
              ? 'Adult'
              : guest['age'] === 'Child'
              ? 'Child'
              : 'Infant')
      )
      data.map((guest) => (guest['birthDate'] = guest['birthDate'].slice(0, 10)))
      data.map((guest) => delete guest['comment'])
    }
  }

  configurePage(data, type)
  const navigate = useNavigate()

  function redirect(bookingId) {
    navigate(`/booking/${bookingId}`)
  }

  function CreateTableHead() {
    if (type === 'Guests') {
      return (
        <thead className="text-center align-middle">
          <tr>
            {guestTableHeader.map((head) => (
              <th key={head}>{head}</th>
            ))}
            {data.status !== 1 ? (
              <>
                <th>Edit</th>
                <th>Delete</th>
              </>
            ) : (
              <></>
            )}
          </tr>
        </thead>
      )
    }
    if (type !== 'Booking') {
      return (
        <thead className="text-center align-middle">
          <tr>
            {guestTableHeader.map((head) => (
              <th key={head}>{head}</th>
            ))}
          </tr>
        </thead>
      )
    }
    return (
      <thead className="text-center align-middle">
        <tr>
          {bookinTableHeader.map((head) => (
            <th key={head}>{head}</th>
          ))}
        </tr>
      </thead>
    )
  }

  function CreateTable() {
    if (type === 'Guests') {
      return guests.map((item) => (
        <tbody key={item}>
          <tr className="text-center align-middle" key={item}>
            {Object.values(item).map((val) => (
              <td key={val}>{val}</td>
            ))}
            {data.status !== 1 ? (
              <>
                <td>
                  <Link to={`/guest/${item.id}`}>
                    <button className="btn btn-outline-secondary">
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-pen"
                        viewBox="0 0 16 16">
                        <path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001zm-.644.766a.5.5 0 0 0-.707 0L1.95 11.756l-.764 3.057 3.057-.764L14.44 3.854a.5.5 0 0 0 0-.708l-1.585-1.585z"></path>
                      </svg>
                    </button>
                  </Link>
                </td>
                <td>
                  <button
                    className="btn btn-outline-danger"
                    onClick={() => {
                      deleteApi(`/guestapi/${item.id}`)
                        .then(() => {
                          OnClick()
                        })
                        .catch(() => {
                          navigate('/error')
                        })
                    }}>
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      width="16"
                      height="16"
                      fill="currentColor"
                      className="bi bi-trash"
                      viewBox="0 0 16 16">
                      <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z" />
                      <path
                        fillRule="evenodd"
                        d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"
                      />
                    </svg>
                  </button>
                </td>
              </>
            ) : (
              <></>
            )}
          </tr>
        </tbody>
      ))
    }

    if (type !== 'Booking') {
      return data.map((item) => (
        <tbody key={item.id}>
          <tr className="text-center align-middle" key={item}>
            {Object.values(item).map((val) => (
              <td key={val}>{val}</td>
            ))}
          </tr>
        </tbody>
      ))
    }

    return data.map((item) => (
      <tbody key={item.id}>
        <tr onClick={() => redirect(item.id)} className="text-center align-middle" key={item.id}>
          {Object.values(item).map((val) => (
            <td key={val}>{val}</td>
          ))}
        </tr>
      </tbody>
    ))
  }

  return (
    <>
      {data.length !== 0 ? (
        <table className="table table-sm table-responsive table-striped table-success table-hover align-middle table-bordered border-primary">
          {CreateTableHead()}
          {CreateTable()}
        </table>
      ) : (
        <></>
      )}
    </>
  )
}
export default BookingTable
