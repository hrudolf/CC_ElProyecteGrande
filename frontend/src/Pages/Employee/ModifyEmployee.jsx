import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import "./CreateEmployee.css";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

const ModifyEmployee = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [employeeTypeList, setEmployeeTypeList] = useState("");
  const [shiftList, setShiftList] = useState("");
  const { id } = useParams();

  const [employee, setEmployee] = useState({
    id: id,
    firstName: "",
    lastName: "",
    dateOfBirth: "",
    preferredShift: "",
    workingDays: 0,
    totalVacationDays: 0,
    employeeType: 0,
    monthlyGrossSalary: 0,
    isActive: true,
  });

  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");

    fetch(`https://localhost:44353/api/Employee/${id}`, {
      method: "GET",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        console.log(json);
        setEmployee(json);
      })
      .catch((err) => setError(err));
  }, [id]);

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");

    fetch("https://localhost:44353/api/employeetype", {
      method: "GET",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeTypeList(json);
      })
      .catch((err) => setError(err));
  }, []);

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");

    fetch("https://localhost:44353/api/Shift", {
      method: "GET",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setShiftList(json);
      })
      .catch((err) => setError(err));
  }, []);

  const patchemployee = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");
    setError("");

    const url = `https://localhost:44353/api/Employee/${employee.id}`;
    const fetchMethod = "PATCH";
    const headers = { "Content-Type": "application/json" };
    console.log(employee);
    const response = await fetch(url, {
      method: fetchMethod,
      credentials: "include",
      headers: headers,
      body: JSON.stringify(employee),
    });

    const json = await response.json();
    if (!response.ok) {
      setLoading(false);
      setError(json.error);
    } else {
      setMessage("Saved, you will be redirected.");
      setTimeout(() => navigate("/employees"), 1000);
    }
  };

  const updateProperty = (input, key) => {
    const employeeCopy = JSON.parse(JSON.stringify(employee));
    
    if (key === "employeeType") {
      input = employeeTypeList.find(e => e.id === +input);
    }

    if (key === "preferredShift") {
      input = shiftList.find(e => e.id === +input);
    }

    employeeCopy[key] = input;

    setEmployee(employeeCopy);
    console.log(employeeCopy);
  };

  return (
    <div className="container bg-light w-50 p-3">
      <h1 className="text-center">Change Employee Details</h1>
      <div className="container align-items-left w-75">
        {!message && (
          <form className="UserForm" onSubmit={patchemployee}>
            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="firstname">First Name:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="text"
                    name="firstname"
                    id="firstname"
                    value={employee.firstName}
                    onChange={(e) => updateProperty(e.target.value, "firstName")}
                    required
                  />
                </Col>
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="lastname">Last Name:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="text"
                    name="lastname"
                    id="lastname"
                    value={employee.lastName}
                    onChange={(e) => updateProperty(e.target.value, "lastName")}
                    required
                  />
                </Col>
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="birthdate">Date of birth:</label>
                </Col>
                <Col>
                  <input
                    type="date"
                    className="w-100"
                    name="birthdate"
                    id="birthdate"
                    min="1920-01-01"
                    max="2023-04-20"
                    value={employee.dateOfBirth.slice(0, 10)}
                    onChange={(e) => updateProperty(e.target.value, "dateOfBirth")}
                  />
                </Col>
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                {shiftList && (
                  <>
                    <Col>
                      <label htmlFor="shift">Preferred shift:</label>
                    </Col>
                    <Col>
                      <select
                        name="shift"
                        className="w-100"
                        id="shift"
                        value={employee.preferredShift !== null ? employee.preferredShift.id : ""}
                        onChange={(e) => updateProperty(e.target.value, "preferredShift")}
                        required
                      >
                        <option value="" disabled selected>Select preferred shift</option>
                        {shiftList.map((opt) => (
                          <option key={opt.id} value={opt.id}>
                            {opt.nameOfShift}
                          </option>
                        ))}
                      </select>
                    </Col>
                  </>
                )}
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="workdays">Workdays per month:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="number"
                    name="workdays"
                    id="workdays"
                    value={employee.workingDays}
                    onChange={(e) => updateProperty(e.target.value, "workingDays")}
                    required
                  />
                </Col>
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="vacation">Vacation days:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="number"
                    name="vacation"
                    id="vacation"
                    value={employee.totalVacationDays}
                    onChange={(e) => updateProperty(e.target.value, "totalVacationDays")}
                    required
                  />
                </Col>
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                {employeeTypeList && (
                  <>
                    <Col>
                      <label htmlFor="employeeType">Employee Role:</label>
                    </Col>
                    <Col>
                      <select
                        name="employeeType"
                        className="w-100"
                        id="employeeType"
                        value={employee.employeeType !== null ? employee.employeeType.id : ""}
                        onChange={(e) => updateProperty(e.target.value, "employeeType")}
                        required
                      >
                        <option value="" disabled selected>Select employee role</option>
                        {employeeTypeList.map((opt) => (
                          <option key={opt.id} value={opt.id}>
                            {opt.type}
                          </option>
                        ))}
                      </select>
                    </Col>
                  </>
                )}
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="salary">Monthly Salary:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="number"
                    name="salary"
                    id="salary"
                    value={employee.monthlyGrossSalary}
                    onChange={(e) => updateProperty(e.target.value, "monthlyGrossSalary")}
                    required
                  />
                </Col>
              </Row>
            </div>

            <div className="buttons">
              <button
                type="submit"
                className="btn btn-primary w-auto m-1"
                disabled={loading}
              >
                Update Employee Data
              </button>

              <button
                type="button"
                className="btn btn-secondary w-auto m-1"
                onClick={() => navigate("/employees")}
              >
                Cancel
              </button>
            </div>
          </form>
        )}
        {error && (
          <div className="alert alert-danger" role="alert">
            {error ? error : ""}
          </div>
        )}
        {message && (
          <div className="alert alert-success" role="alert">
            {message ? message : ""}
          </div>
        )}
      </div>
    </div>
  );
};

export default ModifyEmployee;
