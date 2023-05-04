import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./VacationRequest.css";
import Spinner from "../Layout/Spinner";

const VacationRequest = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [requestList, setRequestList] = useState("");

  const navigate = useNavigate();

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
    window.location.reload(false);
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
    <div class="container align-items-center">
      <h1 class="p-2 m-2">Vacation Requests</h1>
      <div class="container align-items-center">
        {requestList && (
          <div className="">
            <table class="table table-light table-bordered table-striped table-responsive">
              <thead class="p-2">
                <tr>
                  <th scope="col">Id</th>
                  <th scope="col">First Name</th>
                  <th scope="col">Last Name</th>
                  <th scope="col">Date of Birth</th>
                  <th scope="col">Preferred Shift</th>
                  <th scope="col">Workdays per Week</th>
                  <th scope="col">Vacation Days</th>
                  <th scope="col">Employee Type</th>
                  <th scope="col">Vacation StartDate</th>
                  <th scope="col">Vacation EndDate</th>
                  <th scope="col">Is Approved</th>
                  <th scope="col"></th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody class="p-5">
                {requestList.map((request) => {
                  return (
                    <tr key={request.id}>
                      <th scope="row">{request.id}</th>
                      <td>{request.employee.firstName}</td>
                      <td>{request.employee.lastName}</td>
                      <td>{request.employee.dateOfBirth}</td>
                      <td>{request.employee.preferredShift.nameOfShift}</td>
                      <td>{request.employee.workingDays}</td>
                      <td>{request.employee.totalVacationDays}</td>
                      <td>{request.employee.employeeType.type}</td>
                      <td>{request.startDate}</td>
                      <td>{request.endDate}</td>
                      <td>{request.isApproved}</td>
                      <td>
                        {" "}
                        <button
                          class="btn btn-secondary"
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
                          class="btn btn-warning"
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
        
        {loading && <div><Spinner/></div>}
        {error && <div className={"error"}>{error ? error : ""}</div>}
        {message && <div className={"message"}>{message ? message : ""}</div>}
        {!loading &&<button
          class="btn btn-primary w-auto"
          onClick={() => navigate("/vacationRequests/create")}
        >
          Add a new request
        </button>}
      </div>
    </div>
  );
};

export default VacationRequest;
