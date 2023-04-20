import { Outlet, Link } from "react-router-dom";

import "./Layout.css";

const Layout = () => (
  <div className="Layout">
    <nav className="navbar navbar-expand-sm navbar-light bg-light navbar-responsive">
    <div className="collapse navbar-collapse" id="navbarSupportedContent">
      <ul className="navbar-nav me-auto mb-2 mb-lg-0">
        <li className="nav-item">
          <Link to="/" className="nav-link active" aria-current="page">Home</Link>
        </li>
        <li className="nav-item">
          <Link to="/employeetypes" className="nav-link active" aria-current="page">Employee Roles</Link>
        </li>
        <li className="nav-item">
          <Link to="/login" className="nav-link active" aria-current="page">Login</Link>
        </li>
      </ul>
      </div>
    </nav>
    <Outlet />
  </div>
);

export default Layout;