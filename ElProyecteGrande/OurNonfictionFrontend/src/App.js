import { Link, Outlet } from "react-router-dom";

const App = () => {
  return (
    <>
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
          <div className="container-fluid">
            <a className="navbar-brand" href='/'>Our Nonfiction</a>
              <ul className="navbar-nav flex-grow-1">
                <li className="nav-item">
                  <Link className="nav-link text-dark" to="/">Bookings</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link text-dark" to="/guests">Guests</Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link text-dark" to="/calendar">Calendar</Link>
                </li>
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
