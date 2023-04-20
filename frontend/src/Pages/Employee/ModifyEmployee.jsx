import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
//import "./Createemployee.css";

const ModifyEmployee = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const { id } = useParams();

  const [employee, setEmployee] = useState({
    firstName: "",
    lastName: "",
    dateOfBirth: "",
    preferredShift: [],
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
    fetch(`/api/Employee/${id}`, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployee(json);
      })
      .catch((err) => setError(err));
  }, [id]);

  const postemployee = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");
    setError("");

    const url = `/api/Employee/${id}`;
    const fetchMethod = "PUT";
    const headers = { "Content-Type": "application/json" };
    console.log(employee);
    const response = await fetch(url, {
      method: fetchMethod,
      headers: headers,
      body: JSON.stringify(employee),
    });

    const json = await response.json();
    if (!response.ok) {
      setLoading(false);
      setError(json.error);
    } else {
      setMessage("Employee successfully added, you will be redirected.");
      setTimeout(() => navigate("/employees"), 1000);
    }
  };

  const updateProperty = (input, id) => {
    const employeeCopy = JSON.parse(JSON.stringify(employee));
    switch (id) {
      case 1:
        employeeCopy.firstName = input;
        break;
      case 2:
        employeeCopy.lastName = input;
        break;
      case 3:
        employeeCopy.dateOfBirth = input;
        break;
      case 4:
        employeeCopy.preferredShift = input;
        break;
      case 5:
        employeeCopy.workingDays = input;
        break;
      case 6:
        employeeCopy.totalVacationDays = input;
        break;
      case 7:
        employeeCopy.employeeType = input;
        break;
      case 8:
        employeeCopy.monthlyGrossSalary = input;
        break;
      default:
        console.log(" ");
    }

    setEmployee(employeeCopy);
  };

  return (
    <div className="container align-items-center">
      <h1>Change Employee Details</h1>
      <div className="container align-items-left">
        <form className="UserForm" onSubmit={postemployee}>
          <label htmlFor="employee">First Name:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.firstName}
            onChange={(e) => updateProperty(e.target.value, 1)}
            required
          />

          <label htmlFor="employee">Last Name:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.lastName}
            onChange={(e) => updateProperty(e.target.value, 2)}
            required
          />

          <label htmlFor="employee">Date of birth:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.dateOfBirth}
            onChange={(e) => updateProperty(e.target.value, 3)}
          />

          <label htmlFor="employee">Preferred Shift:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.preferredShift}
            onChange={(e) => updateProperty(e.target.value, 4)}
          />

          <label htmlFor="employee">Workdays per week:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.workingDays}
            onChange={(e) => updateProperty(e.target.value, 5)}
            required
          />

          <label htmlFor="employee">Workdays per week:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.totalVacationDays}
            onChange={(e) => updateProperty(e.target.value, 6)}
            required
          />

          <label htmlFor="employee">Employee Type:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.employeeType}
            onChange={(e) => updateProperty(e.target.value, 7)}
            required
          />

          <label htmlFor="employee">Monthly Salary:</label>
          <input
            type="employee"
            name="employee"
            id="employee"
            value={employee.monthlyGrossSalary}
            onChange={(e) => updateProperty(e.target.value, 8)}
            required
          />

          <div className="buttons">
            <button
              type="submit"
              class="btn btn-primary w-auto m-1"
              disabled={loading}
            >
              Update Employee Data
            </button>

            <button
              type="button"
              class="btn btn-secondary w-auto m-1"
              onClick={() => navigate("/employees")}
            >
              Cancel
            </button>
          </div>
        </form>
        {error && <div className={"error"}>{error ? error : ""}</div>}
        {message && <div className={"message"}>{message ? message : ""}</div>}
      </div>
    </div>
  );
};

export default ModifyEmployee;
