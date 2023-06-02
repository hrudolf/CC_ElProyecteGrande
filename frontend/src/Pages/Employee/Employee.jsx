import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CreateEmployee.css";
import Spinner from "../Layout/Spinner";
import RequestPopUp from "../VacationRequests/RequestPopUp";

const Employee = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [employeeList, setEmployeeList] = useState("");
  const [requestList, setRequestList] = useState("");

  const navigate = useNavigate();

  const DeleteEmployee = (employeeId) => {
    fetch(
      process.env.REACT_APP_APIURL +
        `/api/Employee/temporary-delete/${employeeId}`,
      {
        method: "PATCH",
        credentials: "include",
      }
    )
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeList(
          employeeList.filter((employee) => employee.id !== employeeId)
        );
      })
      .catch((err) => setError(err));
  };

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch(process.env.REACT_APP_APIURL + "/api/employee/active", {
      method: "GET",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeList(json);
      })
      .catch((err) => setError(err));

    fetch(process.env.REACT_APP_APIURL + `/api/VacationRequest/`, {
      method: "GET",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        setRequestList(json);
      })
      .catch((err) => console.log(err));
  }, []);

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Employees
      </h1>
      <div className="container align-items-center">
        {employeeList && (
          <div className="employee">
            <table className="table table-light table-bordered table-striped table-responsive">
              <thead className="p-2" style={{ verticalAlign: "middle" }}>
                <tr>
                  <th scope="col">Id</th>
                  <th scope="col">First Name</th>
                  <th scope="col">Last Name</th>
                  <th scope="col">Date of Birth</th>
                  <th scope="col">Role</th>
                  <th scope="col">Preferred shift</th>
                  <th scope="col" style={{ width: "100px" }}>
                    Workdays per Month
                  </th>
                  <th scope="col">Salary per shift</th>
                  <th scope="col" style={{ textAlign: "center" }}>
                    <button
                      className="btn btn-primary w-auto"
                      onClick={() => navigate("/employees/create")}
                    >
                      Add a new employee
                    </button>
                  </th>
                </tr>
              </thead>
              <tbody className="p-5">
                {employeeList &&
                  requestList &&
                  employeeList.map((employee) => {
                    return (
                      <tr key={employee.id} style={{ verticalAlign: "middle" }}>
                        <th scope="row">{employee.id}</th>
                        <td>{employee.firstName}</td>
                        <td>{employee.lastName}</td>
                        <td>{employee.dateOfBirth.slice(0, 10)}</td>
                        <td>
                          {employee.employeeType === null
                            ? "N/A"
                            : employee.employeeType.type}
                        </td>
                        <td>
                          {employee.preferredShift === null
                            ? "N/A"
                            : employee.preferredShift.nameOfShift}
                        </td>
                        <td style={{ textAlign: "right" }}>
                          {employee.workingDays}
                        </td>
                        <td style={{ textAlign: "right" }}>
                          $
                          {parseInt(employee.salaryPerShift).toLocaleString(
                            "en-US",
                            {
                              valute: "USD",
                            }
                          )}
                        </td>
                        <td>
                          {" "}
                          <RequestPopUp
                            firstName={employee.firstName}
                            lastName={employee.lastName}
                            id={employee.id}
                            vacationDays={employee.totalVacationDays}
                            requests={requestList}
                          />{" "}
                          <button
                            className="btn btn-secondary"
                            onClick={() =>
                              navigate(`/employees/edit/${employee.id}`)
                            }
                            disabled={loading}
                          >
                            Edit
                          </button>{" "}
                          <button
                            className="btn btn-warning"
                            onClick={() => DeleteEmployee(employee.id)}
                            disabled={loading}
                          >
                            Delete
                          </button>{" "}
                        </td>
                      </tr>
                    );
                  })}
                <tr></tr>
              </tbody>
            </table>
          </div>
        )}
        {loading && (
          <div>
            <Spinner />
          </div>
        )}
        {error && <div className={"error"}>{error ? error : ""}</div>}
        {message && <div className={"message"}>{message ? message : ""}</div>}
      </div>
    </div>
  );
};

export default Employee;
