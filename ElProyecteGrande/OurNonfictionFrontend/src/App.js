import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { bookings: [], loading: true };
    }

    componentDidMount() {
        this.populateBookingData();
    }

    static renderBookingsTable(bookings) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Booker's Name</th>
                        <th>Email</th>
                        <th>Country</th>
                    </tr>
                </thead>
                <tbody>
                    {bookings.map(booking =>
                        <tr key={booking.id}>
                            <td>{booking.id}</td>
                            <td>{booking.bookersName}</td>
                            <td>{booking.email}</td>
                            <td>{booking.country}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : App.renderBookingsTable(this.state.bookings);

        return (
            <div>
                <h1 id="tabelLabel" >Bookings</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateBookingData() {
        const response = await fetch('booking');
        const data = await response.json();
        this.setState({ bookings: data, loading: false });
    }
}
