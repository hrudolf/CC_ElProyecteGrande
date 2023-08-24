import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./VacationRequest.css";
import Spinner from "../Layout/Spinner";
import { UserContext } from "../../App";

const VacationRequestPerEmployee = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [requestList, setRequestList] = useState("");
  const { user } = useContext(UserContext);

  const navigate = useNavigate();

  const DeleteRequest = (requestId) => {
    fetch(`https://localhost:7124/api/VacationRequest/${requestId}`, {
      method: "DELETE",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRequestList(
          requestList.filter((request) => request.id !== requestId)
        );
      })
      .catch((err) => setError(err));
  };

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch(`https://localhost:7124/api/VacationRequest/employee/${user.id}`, {
      method: "GET",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRequestList(json);
      })
      .catch((err) => setError(err));
  }, [user.id]);

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Vacation Requests For {`${user.firstName} ${user.lastName}`}
      </h1>
      <div className="container align-items-center">
        {requestList && (
          <div className="tableFixHead align-items-center">
            <table className="table table-light table-bordered table-striped table-responsive">
              <thead className="p-2">
                <tr>
                  <th scope="col">Id</th>
                  <th scope="col">First Name</th>
                  <th scope="col">Last Name</th>
                  <th scope="col">Vacation Days</th>
                  <th scope="col">Employee Type</th>
                  <th scope="col">Vacation StartDate</th>
                  <th scope="col">Vacation EndDate</th>
                  <th scope="col">Approved?</th>
                  <th scope="col"></th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody className="p-5">
                {requestList.map((request) => {
                  return (
                    <tr key={request.id}>
                      <th scope="row">{request.id}</th>
                      <td>{request.employee.firstName}</td>
                      <td>{request.employee.lastName}</td>
                      <td>{request.employee.totalVacationDays}</td>
                      <td>
                        {request.employee.employeeType === null
                          ? "N/A"
                          : request.employee.employeeType.type}
                      </td>
                      <td>{request.startDate.slice(0, 10)}</td>
                      <td>{request.endDate.slice(0, 10)}</td>
                      <td>{request.isApproved ? "Yes" : "No"}</td>
                      <td>
                        {" "}
                        <button
                          className="btn btn-secondary"
                          onClick={() =>
                            navigate(`/vacationrequests/edit/${request.id}`)
                          }
                          disabled={loading}
                        >
                          Edit
                        </button>{" "}
                      </td>
                      <td>
                        {" "}
                        <button
                          className="btn btn-warning"
                          onClick={() => DeleteRequest(request.id)}
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

        <button
          className="btn btn-primary w-auto mt-2"
          onClick={() => navigate("/vacationrequests/create")}
          disabled={loading}
        >
          Add a new request
        </button>
      </div>
      {loading && (
        <div>
          <Spinner />
        </div>
      )}
      {error && <div className={"error"}>{error ? error : ""}</div>}
      {message && <div className={"message"}>{message ? message : ""}</div>}
    </div>
  );
};

export default VacationRequestPerEmployee;
