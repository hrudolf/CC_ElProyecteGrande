import { Outlet, Link } from "react-router-dom";

import "./Layout.css";

const Layout = () => (
  <div className="Layout">
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <Link to="/" class="nav-link active" aria-current="page">Home</Link>
        </li>
        <li class="nav-item">
          <Link to="/employeetypes" class="nav-link active" aria-current="page">Employee Types</Link>
        </li>
        <li class="nav-item">
          <Link to="/login" class="nav-link active" aria-current="page">Login</Link>
        </li>
      </ul>
      </div>
    </nav>
    <Outlet />
  </div>
);

export default Layout;