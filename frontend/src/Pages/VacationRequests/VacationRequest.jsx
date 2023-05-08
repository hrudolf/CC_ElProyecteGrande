import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./VacationRequest.css";
import Spinner from "../Layout/Spinner";

const VacationRequest = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [requestList, setRequestList] = useState("");

  const navigate = useNavigate();

  const ChangeRequestApproval = (requestId) => {
    fetch(`/api/VacationRequest/${requestId}`, {
      method: "PATCH",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRequestList(json);
      })
      .catch((err) => setError(err));
    /* window.location.reload(false); */
  };

  const DeleteRequest = (requestId) => {
    fetch(`/api/VacationRequest/${requestId}`, {
      method: "DELETE",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRequestList(
          requestList.filter((request) => request.id !== requestId)
        );
      })
      .catch((err) => setError(err));
    /*  window.location.reload(false); */
  };

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch("/api/VacationRequest", {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRequestList(json);
      })
      .catch((err) => setError(err));
  }, []);

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Vacation Requests
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
                      <td>{request.employee.employeeType === null ? "N/A" : request.employee.employeeType.type}</td>
                      <td>{request.startDate.slice(0, 10)}</td>
                      <td>{request.endDate.slice(0, 10)}</td>
                      <td>{request.isApproved ? "Yes" : "No"}</td>
                      <td>
                        {" "}
                        <button
                          className="btn btn-info"
                          onClick={() => ChangeRequestApproval(request.id)}
                          disabled={loading}
                        >
                          {request.isApproved ? "Revoke" : "Approve"}
                        </button>{" "}
                      </td>
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
          onClick={() => navigate("/vacationRequests/create")}
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

export default VacationRequest;
