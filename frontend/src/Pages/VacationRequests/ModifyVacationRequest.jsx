import { useState, useEffect, useContext } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import "./VacationRequest.css";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { UserContext } from "../../App";

const ModifyVacationRequest = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const { user } = useContext(UserContext);
  
  const navigate = useNavigate();
  const { id } = useParams();

  const [request, setRequest] = useState({
    employeeId: "",
    startDate: "",
    endDate: ""
  });


  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");

    fetch(`https://localhost:44353/api/VacationRequest/${id}`, {
      method: "GET",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        if (user.id === json.employee.id || ["Admin", "Accountant", "Supervisor"].includes(user.role)) {
          setRequest(json);
        } else {
          navigate("/vacationrequests");
        }
      })
      .then(setLoading(false))
      .catch((err) => setError(err));
  }, [id, user.id, navigate, user.role]);

  const putRequest = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");
    setError("");

    const url = "https://localhost:44353/api/VacationRequest";
    const fetchMethod = "PUT";
    const headers = { "Content-Type": "application/json" };
    console.log(request);
    const response = await fetch(url, {
      method: fetchMethod,
      credentials: "include",
      headers: headers,
      body: JSON.stringify(request),
    });

    const json = await response.json();
    if (!response.ok) {
      setLoading(false);
      setError(json.error);
    } else {
      setMessage("Saved, you will be redirected.");
      setTimeout(() => navigate("/vacationrequests"), 1000);
    }
  };

  const updateProperty = (input, id) => {
    const requestCopy = JSON.parse(JSON.stringify(request));
    requestCopy[id] = input;
    setRequest(requestCopy);
  };


  return (
    <div className="container bg-light w-50 p-2">
      <h1 className="text-center">Change Request Details</h1>
      <div className="container align-items-left w-75">
        {!message && (
          <form className="UserForm" onSubmit={putRequest}>
            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="startDate">Start Date:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="date"
                    name="startDate"
                    id="startDate"
                    value={request.startDate.slice(0, 10)}
                    onChange={(e) => updateProperty(e.target.value, "startDate")}
                    required
                  />
                </Col>
              </Row>
            </div>

            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="endDate">End Date:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="date"
                    name="endDate"
                    id="endDate"
                    min="2023-05-03"
                    max="2025-12-31"
                    value={request.endDate.slice(0, 10)}
                    onChange={(e) => updateProperty(e.target.value, "endDate")}
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
                Update Vacation Request
              </button>

              <button
                type="button"
                className="btn btn-secondary w-auto m-1"
                onClick={() => navigate("/vacationrequests")}
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

export default ModifyVacationRequest;
