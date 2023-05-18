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

export const UserContext = createContext("user");

const App = () => {

    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setLoading(true);

        async function fetchData() {
            const response = await fetch("/login", {
                method: "GET"
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

    return (
        <div className="App">
            {!loading &&
                <UserContext.Provider value={{user, setUser}}>
                    <Routes>
                        <Route path="/" element={<Layout/>}>

                            <Route path="/" element={<HomePage/>}></Route>
                            <Route path="/about" element={<About/>}></Route>

                            <Route path="/employees">
                                <Route path="" element={<Employee/>}/>
                                <Route path="create" element={<CreateEmployee/>}></Route>
                                <Route path="edit/:id" element={<ModifyEmployee/>}></Route>
                            </Route>

                            <Route path="/employeetypes" element={<EmployeeTypes/>}/>
                            <Route path="/shifts" element={<Shifts/>}/>
                            <Route path="/myshifts" element={<MyShifts/>}/>
                            <Route path="/roster" element={<Roster/>}/>

                            <Route path="/vacationrequests">
                                <Route path="" element={<VacationRequest/>}/>
                                <Route path="create" element={<CreateVacationRequest/>}/>
                                <Route path="edit/:id" element={<ModifyVacationRequest/>}/>
                            </Route>

                            <Route path="/vacationrequests/employee">
                                <Route path="" element={user !== null ? <VacationRequestPerEmployee/> :
                                    <Navigate to="/login"/>}/>
                                <Route path="create" element={<CreateVacationRequest/>}/>
                                <Route path="edit/:id" element={<ModifyVacationRequest/>}/>
                            </Route>

                            <Route path="/login" element={<LoginPage/>}/>
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
