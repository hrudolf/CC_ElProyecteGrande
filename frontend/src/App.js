import Layout from "./Pages/Layout";
import NotFoundPage from "./Pages/NotFoundPage";
import HomePage from "./Pages/HomePage";
import EmployeeTypes from "./Pages/EmployeeTypes/EmployeeTypes";
import { Routes, Route } from "react-router-dom";
import Employee from "./Pages/Employee/Employee";
import CreateEmployee from "./Pages/Employee/CreateEmployee";
import ModifyEmployee from "./Pages/Employee/ModifyEmployee";
import VacationRequest from "./Pages/VacationRequests/VacationRequest";
import CreateVacationRequest from "./Pages/VacationRequests/CreateVacationRequest";
import ModifyVacationRequest from "./Pages/VacationRequests/ModifyVacationRequest";


function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/" element={<HomePage />}></Route>
          <Route path="/employees">
            <Route path="" element={<Employee />} />
            <Route path="create" element={<CreateEmployee />}></Route>
            <Route path="edit/:id" element={<ModifyEmployee />}></Route>
          </Route>
          <Route path="/employeetypes">
            <Route path="" element={<EmployeeTypes />} />
          </Route>
          <Route path="/vacationrequests">
            <Route path="" element={<VacationRequest />} />
            <Route path="create" element={<CreateVacationRequest />}></Route>
            <Route path="edit/:id" element={<ModifyVacationRequest />}></Route>
          </Route>
        <Route path="*" element={<NotFoundPage />}></Route>
        </Route>
      </Routes>
    </div>
  );
}

export default App;
