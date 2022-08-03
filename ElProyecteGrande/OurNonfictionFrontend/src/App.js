import { Link, Outlet } from "react-router-dom";

const App = () => {

 return (
    <div>
        <nav
        style={{
          borderBottom: "solid 1px",
          paddingBottom: "1rem",
        }}
      >
        <Link to="/">Bookings</Link>{"     "}          
        <Link to="/guests">Guests</Link>{" "}
        <Link to="/newbooking">Add New Booking</Link>
      </nav>
      <Outlet/>

      <footer className="border-top footer text-muted">
        <div className="container">
            &copy; 2022 - El Proyecte Grande - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
        </div>
    </footer>
    </div>
  )
}

export default App


    