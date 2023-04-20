import Layout from "./Pages/Layout";
import NotFoundPage from "./Pages/NotFoundPage";
import HomePage from "./Pages/HomePage";
import EmployeeTypes from "./Pages/EmployeeTypes/EmployeeTypes";
import { Routes, Route } from "react-router-dom";
import CreateEmployeeType from "./Pages/EmployeeTypes/CreateEmployeeType";
import ModifyEmployeeType from "./Pages/EmployeeTypes/ModifyEmployeeType";
import Employee from "./Pages/Employee/Employee";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/" element={<HomePage />}></Route>
          <Route path="/employees">
            <Route path="" element={<Employee />} />
          </Route>
          <Route path="/employeetypes">
            <Route path="" element={<EmployeeTypes />} />
            <Route path="create" element={<CreateEmployeeType />}></Route>
            <Route path="edit/:id" element={<ModifyEmployeeType />}></Route>
          </Route>
        </Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
  );
}

export default App;
