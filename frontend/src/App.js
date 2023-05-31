import {useState, createContext, useEffect} from "react";
import {Routes, Route, Navigate} from "react-router-dom";
import Layout from "./Pages/Layout";
import NotFoundPage from "./Pages/NotFoundPage";
import HomePage from "./Pages/HomePage";
import About from "./Pages/About";
import Spinner from "./Pages/Layout/Spinner";
import Employee from "./Pages/Employee/Employee";
import CreateEmployee from "./Pages/Employee/CreateEmployee";
import ModifyEmployee from "./Pages/Employee/ModifyEmployee";
import EmployeeTypes from "./Pages/EmployeeTypes/EmployeeTypes";
import VacationRequest from "./Pages/VacationRequests/VacationRequest";
import VacationRequestPerEmployee from "./Pages/VacationRequests/VacationRequestPerEmployee";
import CreateVacationRequest from "./Pages/VacationRequests/CreateVacationRequest";
import ModifyVacationRequest from "./Pages/VacationRequests/ModifyVacationRequest";
import Shifts from "./Pages/Shift/Shifts";
import MyShifts from "./Pages/Roster/MyShifts";
import Roster from "./Pages/Roster/Roster";
import LoginPage from "./Pages/LoginPage";
import EmployeePublic from "./Pages/Employee/EmployeePublic";
import RosterPublic from "./Pages/Roster/RosterPublic";

export const UserContext = createContext("user");

const App = () => {

    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setLoading(true);

        async function fetchData() {
            const response = await fetch(process.env.REACT_APP_APIURL + "/login", {
                method: "GET",
                credentials: "include"
            });
            const json = await response.json();
            console.log(json);
            if (!response.ok) {
                setLoading(false);
            } else {
                setLoading(false);
                setUser(json);
                console.log(json);
            }
        }

        fetchData();
    }, []);

    const isAuthenticated = (allowedRoles) => {
        if (user === null) return false;
        if (allowedRoles.filter(element => user.role === element).length > 0) {
            return true;
        }
        return false;
    }

    return (
        <div className="App">
            {!loading &&
                <UserContext.Provider value={{user, setUser}}>
                    <Routes>
                        <Route path="/" element={<Layout/>}>

                            <Route path="/" element={<HomePage/>}></Route>
                            <Route path="/about" element={<About/>}></Route>

                            <Route path="/employees">
                                <Route path="" element={(isAuthenticated(["Admin", "Supervisor", "Accountant"]) &&
                                    <Employee/>) || (isAuthenticated(["Admin", "Basic", "ShiftLead", "Supervisor", "Accountant"]) &&
                                    <EmployeePublic/>) || <Navigate to="/"/>}/>
                                <Route path="create" element={(isAuthenticated(["Admin", "Supervisor", "Accountant"]) &&
                                    <CreateEmployee/>) || <Navigate to="/"/>}/>
                                <Route path="edit/:id"
                                       element={(isAuthenticated(["Admin", "Supervisor", "Accountant"]) &&
                                           <ModifyEmployee/>) || <Navigate to="/"/>}/>
                            </Route>

                            <Route path="/employeetypes"
                                   element={(isAuthenticated(["Admin"]) && <EmployeeTypes/>) || <Navigate to="/"/>}/>
                            <Route path="/shifts" element={(isAuthenticated(["Admin", "Supervisor"]) && <Shifts/>) ||
                                <Navigate to="/"/>}/>
                            <Route path="/myshifts"
                                   element={(isAuthenticated(["Admin", "Basic", "ShiftLead", "Supervisor"]) &&
                                       <MyShifts/>) || <Navigate to="/"/>}/>
                            <Route path="/roster"
                                   element={(isAuthenticated(["Admin", "ShiftLead", "Supervisor"]) &&
                                       <Roster/>) || (isAuthenticated(["Admin", "Basic", "ShiftLead", "Supervisor"]) &&
                                       <RosterPublic />) ||<Navigate to="/"/>}/>

                            <Route path="/vacationrequests">
                                <Route path=""
                                       element={(isAuthenticated(["Admin", "Supervisor"]) && <VacationRequest/>) ||
                                           <Navigate to="/vacationrequests/employee"/>}/>
                                <Route path="employee"
                                       element={(isAuthenticated(["Admin", "Basic", "ShiftLead", "Supervisor"]) &&
                                               <VacationRequestPerEmployee/>) ||
                                           <Navigate to="/"/>}/>
                                <Route path="create"
                                       element={(isAuthenticated(["Admin", "Basic", "ShiftLead", "Supervisor"]) &&
                                           <CreateVacationRequest/>) || <Navigate to="/"/>}/>
                                <Route path="edit/:id"
                                       element={(isAuthenticated(["Admin", "Basic", "ShiftLead", "Supervisor"]) &&
                                           <ModifyVacationRequest/>) || <Navigate to="/"/>}/>
                            </Route>

                            <Route path="/login" element={user === null ? <LoginPage/> : <Navigate to="/"/>}/>
                            <Route path="*" element={<NotFoundPage/>}/>
                        </Route>
                    </Routes>
                </UserContext.Provider>
            }
            {loading && <Spinner/>}

        </div>
    );
}

export default App;
