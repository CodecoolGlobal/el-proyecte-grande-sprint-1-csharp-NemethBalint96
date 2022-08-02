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
        <Link to="/guests">Guests</Link>
      </nav>
      <Outlet/>

      <di>
        <p>Hello Footer!</p>
      </di>
    </div>
  )
}

export default App


    