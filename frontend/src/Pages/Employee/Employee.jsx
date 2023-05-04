import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CreateEmployee.css";
import Spinner from "../Layout/Spinner";

const Employee = () => {
  const [loading, setLoading] = useState(true);
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
    fetch("/api/employee", {
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
                  <th scope="col" style={{ verticalAlign: "top" }}>
                    Id
                  </th>
                  <th scope="col" style={{ verticalAlign: "top" }}>
                    First Name
                  </th>
                  <th scope="col" style={{ verticalAlign: "top" }}>
                    Last Name
                  </th>
                  <th scope="col" style={{ verticalAlign: "top" }}>
                    Date of Birth
                  </th>
                  <th scope="col" style={{ verticalAlign: "top" }}>
                    Role
                  </th>
                  <th scope="col" style={{ verticalAlign: "top" }}>
                    Preferred shift
                  </th>
                  <th scope="col" style={{ width: "100px", textAlign: "top" }}>
                    Workdays per Month
                  </th>
                  <th
                    scope="col"
                    style={{
                      width: "80px",
                      textAlign: "center",
                      verticalAlign: "top",
                    }}
                  >
                    Vacation Days
                  </th>
                  <th scope="col" style={{ verticalAlign: "top" }}>
                    Monthly Salary
                  </th>
                  <th scope="col">
                  {!loading &&<button
                  class="btn btn-primary w-auto"
                  onClick={() => navigate("/employees/create")}
                  >Add a new employee
                  </button>}
                  </th>
                </tr>
              </thead>
              <tbody class="p-5">
                {employeeTypeList &&
                  employeeList.map((employee) => {
                    return (
                      <tr key={employee.id} style={{ verticalAlign: "middle" }}>
                        <th scope="row">{employee.id}</th>
                        <td>{employee.firstName}</td>
                        <td>{employee.lastName}</td>
                        <td>{employee.dateOfBirth.slice(0, 10)}</td>
                        <td>{employee.employeeType.type}</td>
                        <td>{employee.preferredShift.nameOfShift}</td>
                        <td style={{ textAlign: "right" }}>
                          {employee.workingDays}
                        </td>
                        <td style={{ textAlign: "right" }}>
                          {employee.totalVacationDays}
                        </td>
                        <td style={{ textAlign: "right" }}>
                          $
                          {parseInt(employee.monthlyGrossSalary).toLocaleString(
                            "en-US",
                            {
                              valute: "USD",
                            }
                          )}
                        </td>
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
                          <button
                            class="btn btn-secondary"
                            onClick={() =>
                              navigate(`/employees/edit/${employee.id}`)
                            }
                            disabled={loading}
                          >
                            Vacation requests
                          </button>{" "}
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
        {loading && <div><Spinner/></div>}
        {error && <div className={"error"}>{error ? error : ""}</div>}
        {message && <div className={"message"}>{message ? message : ""}</div>}
        </div>
    </div>
  );
};

export default Employee;
