import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import "./CreateEmployee.css";

const CreateEmployee = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [employeeTypeList, setEmployeeTypeList] = useState("");

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
    fetch("/api/employeetype", {
      method: "GET"
    })
      .then(res => res.json())
      .then(json => {
        setLoading(false);
        setEmployeeTypeList(json);
      })
      .catch(err => setError(err))

  }, []);

  const postemployee = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");
    setError("");

    const url = "/api/Employee";
    const fetchMethod = "POST";
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
      setMessage("Saved, you will be redirected.");
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
    <div className="container bg-light w-25 p-2">
      <h1 class="text-center">Add Employee</h1>
      <div className="container align-items-left">
        {!message &&
          <form className="UserForm" onSubmit={postemployee}>
            <label htmlFor="firstname">First Name:</label>
            <input
              type="text"
              name="firstname"
              id="firstname"
              value={employee.firstName}
              onChange={(e) => updateProperty(e.target.value, 1)}
              required
            />

            <label htmlFor="lastname">Last Name:</label>
            <input
              type="text"
              name="lastname"
              id="lastname"
              value={employee.lastName}
              onChange={(e) => updateProperty(e.target.value, 2)}
              required
            />

            <label htmlFor="birthdate">Date of birth:</label>
            <input
              type="date"
              name="birthdate"
              id="birthdate"
              min="1920-01-01"
              max="2023-04-20"
              value={employee.dateOfBirth}
              onChange={(e) => updateProperty(e.target.value, 3)}
            />

            <label htmlFor="shift">Preferred Shift:</label>
            <input
              type="number"
              name="shift"
              id="shift"
              value={employee.preferredShift}
              onChange={(e) => updateProperty(e.target.value, 4)}
              required
            />

            <label htmlFor="workdays">Workdays per week:</label>
            <input
              type="number"
              name="workdays"
              id="workdays"
              value={employee.workingDays}
              onChange={(e) => updateProperty(e.target.value, 5)}
              required
            />

            <label htmlFor="vacation">Vacation days per year:</label>
            <input
              type="number"
              name="vacation"
              id="vacation"
              value={employee.totalVacationDays}
              onChange={(e) => updateProperty(e.target.value, 6)}
              required
            />

            {employeeTypeList && <>
              <label htmlFor="employeeType">Employee Type:</label>
              <select
                name="employeeType"
                id="employeeType"
                value={employee.employeeType}
                onChange={(e) => updateProperty(e.target.value, 7)}
                required>
                {employeeTypeList.map((opt) => <option key={opt.id} value={opt.id}>{opt.type}</option>)}
              </select>
            </>}

            <label htmlFor="salary">Monthly Salary:</label>
            <input
              type="number"
              name="salary"
              id="salary"
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
                Create new Employee
              </button>

              <button
                type="button"
                class="btn btn-secondary w-auto m-1"
                onClick={() => navigate("/employees")}
              >
                Cancel
              </button>
            </div>
          </form>}
        {error && <div class="alert alert-danger" role="alert" >{error ? error : ""}</div>}
        {message && <div class="alert alert-success" role="alert" >{message ? message : ""}</div>}
      </div>
    </div>
  );
};

export default CreateEmployee;
