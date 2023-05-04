import { Outlet } from "react-router-dom";
import './HomePage.css'

const HomePage = () => (
  <div >
    <div className="container heropic">
      <img
        src={require("./img/rooster.png")}
        alt="Green Rooster"
        className="rounded mx-auto d-block p-3"
      />
      <h1 className="text-center">Green Rooster</h1>
      <h6 className="text-center p-3">
        ROSTERING | ATTENDANCE | VACATION REQUEST | PAYROLL FORECAST
      </h6>
    </div>
    <Outlet />
  </div>
);

export default HomePage;
