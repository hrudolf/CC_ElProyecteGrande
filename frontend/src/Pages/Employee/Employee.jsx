import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CreateEmployee.css";

const Employee = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [employeeList, setEmployeeList] = useState("");
  const [employeeTypeList, setEmployeeTypeList] = useState("");

  const navigate = useNavigate();

  const DeleteEmployee = (employeeId) => {
    fetch(`/api/Employee/temporary-delete/${employeeId}`, {
      method: "DELETE",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeList(
          employeeList.filter((employee) => employee.id !== employeeId)
        );
      })
      .catch((err) => setError(err));
    window.location.reload(false);
  };

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch("/api/employee/all-employees", {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeList(json);
      })
      .catch((err) => setError(err));

    fetch("/api/employeetype", {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeTypeList(json);
      })
      .catch((err) => setError(err));
  }, []);

  return (
    <div class="container align-items-center">
      <h1 class="p-2 m-2">Employees</h1>
      <div class="container align-items-center">
        {employeeList && (
          <div className="employeeTypes">
            <table class="table table-light table-bordered table-striped table-responsive">
              <thead class="p-2">
                <tr>
                  <th scope="col">Id</th>
                  <th scope="col">First Name</th>
                  <th scope="col">Last Name</th>
                  <th scope="col">Date of Birth</th>
                  <th scope="col">Workdays per Week</th>
                  <th scope="col">Vacation Days</th>
                  <th scope="col">Monthly Salary</th>
                  <th scope="col"></th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody class="p-5">
                {employeeTypeList &&
                  employeeList.map((employee) => {
                    return (
                      <tr key={employee.id}>
                        <th scope="row">{employee.id}</th>
                        <td>{employee.firstName}</td>
                        <td>{employee.lastName}</td>
                        <td>{employee.dateOfBirth}</td>
                        <td>{employee.workingDays}</td>
                        <td>{employee.totalVacationDays}</td>
                        <td>{employee.monthlyGrossSalary}</td>
                        <td>
                          {" "}
                          <button
                            class="btn btn-secondary"
                            onClick={() =>
                              navigate(`/employees/edit/${employee.id}`)
                            }
                            disabled={loading}
                          >
                            Edit
                          </button>{" "}
                        </td>
                        <td>
                          {" "}
                          <button
                            class="btn btn-warning"
                            onClick={() => DeleteEmployee(employee.id)}
                            disabled={loading}
                          >
                            Delete
                          </button>{" "}
                        </td>
                      </tr>
                    );
                  })}
              </tbody>
            </table>
          </div>
        )}
        {loading && <div className={"loading"}>Loading...</div>}
        {error && <div className={"error"}>{error ? error : ""}</div>}
        {message && <div className={"message"}>{message ? message : ""}</div>}
        <button
          class="btn btn-primary w-auto"
          onClick={() => navigate("/employees/create")}
        >
          Add a new employee
        </button>
      </div>
    </div>
  );
};

export default Employee;
