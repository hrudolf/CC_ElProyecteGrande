import { Outlet } from "react-router-dom";
import "./Layout.css";
import LoginStatus from "./LoginStatus";
import { UserContext } from "../../App";
import { useContext, useEffect, useState } from "react";
import Navbaritem from "./Navbaritem";

const Layout = () => {
  const { user } = useContext(UserContext);
  const [navbarFiltered, setNavbarFiltered] = useState(null);

  const navBarList = [
    { path: "/", title: "Home" },
    { path: "/about", title: "About" },
    { path: "/myshifts", title: "My Shifts", roles: ["Admin", "Basic", "ShiftLead", "Supervisor"] },
    { path: "/roster", title: "Roster", roles: ["Admin", "ShiftLead", "Supervisor"] },
    { path: "/employees", title: "Employees", roles: ["Admin", "Supervisor", "Accountant"] },
    { path: "/employeetypes", title: "Employee Roles", roles: ["Admin"] },
    { path: "/shifts", title: "Shift types", roles: ["Admin", "Supervisor"] },
    { path: "/vacationrequests", title: "All Vacation Request", roles: ["Admin", "Supervisor"] },
    { path: "/vacationrequests/employee", title: "My Vacation Requests", roles: ["Admin", "ShiftLead", "Basic", "Supervisor"] }
  ]

  useEffect(() => {
    if (user === null) {
      let filteredNavbar = navBarList.filter(item => !('roles' in item));
      setNavbarFiltered(filteredNavbar);
    } else {
      let filteredNavbar = navBarList.filter(item => {
        if ('roles' in item) {
          const intersection = item.roles.filter(element => user.role === element);
          if (intersection.length > 0) return true;
          return false;
        } else {
          return true;
        }
      });
      setNavbarFiltered(filteredNavbar);
    }
  }, [user])

  return (
    <div className="Layout">
      <nav className="navbar navbar-expand-sm navbar-light bg-light navbar-responsive sticky-top">
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            {navbarFiltered && navbarFiltered.map((item, idx) => <Navbaritem key={idx} item={item}></Navbaritem>)}
          </ul>
        </div>
        <LoginStatus />
      </nav>
      <Outlet />
    </div>
  )
};


export default Layout;
