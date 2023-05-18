import Layout from "./Pages/Layout";
import NotFoundPage from "./Pages/NotFoundPage";
import HomePage from "./Pages/HomePage";
import EmployeeTypes from "./Pages/EmployeeTypes/EmployeeTypes";
import { Routes, Route, Navigate } from "react-router-dom";
import Employee from "./Pages/Employee/Employee";
import CreateEmployee from "./Pages/Employee/CreateEmployee";
import ModifyEmployee from "./Pages/Employee/ModifyEmployee";
import VacationRequest from "./Pages/VacationRequests/VacationRequest";
import VacationRequestPerEmployee from "./Pages/VacationRequests/VacationRequestPerEmployee";
import CreateVacationRequest from "./Pages/VacationRequests/CreateVacationRequest";
import ModifyVacationRequest from "./Pages/VacationRequests/ModifyVacationRequest";
import Shifts from "./Pages/Shift/Shifts";
import Roster from "./Pages/Roster/Roster";
import LoginPage from "./Pages/LoginPage";
import { useState, createContext, useEffect } from "react";
import Spinner from "./Pages/Layout/Spinner";
import MyShifts from "./Pages/Roster/MyShifts";

export const UserContext = createContext("user");

function App() {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    /*     setMessage("");
        setError(""); */
    async function fetchData() {
      const response = await fetch("/login", {
        method: "GET",
      });
      const json = await response.json();
      console.log(json);
      if (!response.ok) {
        setLoading(false);
        /* setError(json.Message);
        setMessage(''); */
      } else {
        setLoading(false);
        /* setError('');
        setMessage('Successful login'); */
        setUser(json);
        console.log(json);
      }
    }

    fetchData();
  }, []);

  return (
    <div className="App">
      {!loading && (
        <UserContext.Provider value={{ user, setUser }}>
          <Routes>
            <Route path="/" element={<Layout />}>
              <Route path="/" element={<HomePage />}></Route>
              <Route path="/employees">
                <Route path="" element={<Employee />} />
                <Route path="create" element={<CreateEmployee />}></Route>
                <Route path="edit/:id" element={<ModifyEmployee />}></Route>
              </Route>
              <Route path="/employeetypes" element={<EmployeeTypes />} />
              <Route path="/shifts" element={<Shifts />} />
              <Route path="/roster" element={<Roster />} />
              <Route path="/myshifts" element={<MyShifts />} />
              <Route path="/vacationrequests">
                <Route path="" element={<VacationRequest />} />
                <Route
                  path="create"
                  element={<CreateVacationRequest />}
                ></Route>
                <Route
                  path="edit/:id"
                  element={<ModifyVacationRequest />}
                ></Route>
              </Route>
              <Route path="/vacationrequests/employee">
                <Route
                  path=""
                  element={
                    user !== null ? (
                      <VacationRequestPerEmployee />
                    ) : (
                      <Navigate to="/login"></Navigate>
                    )
                  }
                />
                <Route
                  path="create"
                  element={<CreateVacationRequest />}
                ></Route>
                <Route
                  path="edit/:id"
                  element={<ModifyVacationRequest />}
                ></Route>
              </Route>
              <Route path="/login" element={<LoginPage />} />
              <Route path="*" element={<NotFoundPage />}></Route>
            </Route>
          </Routes>
        </UserContext.Provider>
      )}
      {loading && <Spinner />}
    </div>
  );
}

export default App;
