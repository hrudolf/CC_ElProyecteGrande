import Layout from "./Pages/Layout";
import NotFoundPage from "./Pages/NotFoundPage";
import HomePage from "./Pages/HomePage";
import EmployeeTypes from "./Pages/EmployeeTypes/EmployeeTypes";
import { Routes, Route } from "react-router-dom";
import CreateEmployeeType from "./Pages/EmployeeTypes/CreateEmployeeType";

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route path="/" element={<HomePage />}></Route>
          <Route path="/employeetypes" element={<EmployeeTypes />}></Route>
          <Route path="/employeetypes/create" element={<CreateEmployeeType />}></Route>
        </Route>
        <Route path="*" element={<NotFoundPage />}></Route>
      </Routes>
    </div>
  );
}

export default App;