import { Outlet, NavLink } from "react-router-dom";

const App = ({ username, setUsername }) => {
  return (
      <>
        <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-primary border-bottom box-shadow mb-3">
          <div className="container-fluid">
            <a className="navbar-brand" id="nonfiction" href='/'>Our Nonfiction</a>
            <ul className="navbar-nav flex-grow-1">
              <li className="nav-item">
                <NavLink className='nav-link' to="/bookings">Bookings</NavLink>
              </li>
              <li className="nav-item">
                <NavLink  className='nav-link' to="/guests">Guests</NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link " to="/calendar">Calendar</NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link " to="/users">Users</NavLink>
              </li>
              {username ? 
              <>
              <li className="nav-item">
                <div className="nav-link">{username}</div>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link " to="/" onClick={() => {
                  setUsername(null);
                  sessionStorage.clear();
                }}>Logout</NavLink>
              </li>
              </>
              :
              <>
              <li className="nav-item">
                <NavLink className="nav-link " to="/">Login</NavLink>
              </li>
              <li className="nav-item">
                <NavLink className="nav-link " to="/registration">Registration</NavLink>
              </li>
              </>}
            </ul>
          </div>
        </nav>
      </header>
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
