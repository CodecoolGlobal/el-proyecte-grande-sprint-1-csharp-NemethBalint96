import { Link, Outlet } from "react-router-dom";

const App = () => {

 return (
    <>
      <nav
        style={{
          borderBottom: "solid 1px",
          paddingBottom: "1rem",
        }}
      >
        <Link to="/">Bookings</Link>{" "}          
        <Link to="/guests">Guests</Link>{" "}
        <Link to="/newbooking">Add New Booking</Link>
      </nav>
      <Outlet/>
      <footer className="footer mt-auto py-3 bg-ligh border-top text-muted">
        <div className="container text-center">
            &copy; 2022 - El Proyecte Grande - Nonfiction
        </div>
      </footer>
    </>
  )
}

export default App
