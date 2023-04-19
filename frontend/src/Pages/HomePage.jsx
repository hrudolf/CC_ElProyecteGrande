import { Outlet } from "react-router-dom";

const HomePage = () => (
    <div className="Layout">
        <div>
            <h1>Lorem ipsum</h1>
            <p>Lorem ipsum</p>
            <h2>Lorem ipsum</h2>
            <p>Lorem ipsum</p>
            <h3>Lorem ipsum</h3>
            <p>Lorem ipsum</p>
            <h4>Lorem ipsum</h4>
            <p>Lorem ipsum</p>
            <h5>Lorem ipsum</h5>
            <p>Lorem ipsum</p>
            <h6>Lorem ipsum</h6>
            <p>Lorem ipsum</p>
        </div>
        <Outlet />
    </div>
);


export default HomePage;