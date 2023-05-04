import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import "./VacationRequest.css";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

const ModifyVacationRequest = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  
  const { id } = useParams();

  const [request, setRequest] = useState({
    employeeId: "",
    startDate: "",
    endDate: ""
  });

  const navigate = useNavigate();
  
  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");

    fetch(`/api/VacationRequest/${id}`, {
      method: "GET",
    })
    .then((res) => res.json())
    .then((json) => {
      setRequest(json);
      })
    .then(setLoading(false))
    .catch((err) => setError(err));
  }, [id]);

  const putRequest = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");
    setError("");

    const url = "/api/VacationRequest";
    const fetchMethod = "PUT";
    const headers = { "Content-Type": "application/json" };
    console.log(request);
    const response = await fetch(url, {
      method: fetchMethod,
      headers: headers,
      body: JSON.stringify(request),
    });

    const json = await response.json();
    if (!response.ok) {
      setLoading(false);
      setError(json.error);
    } else {
      setMessage("Saved, you will be redirected.");
      setTimeout(() => navigate("/vacationRequests"), 1000);
    }
  };

  const updateProperty = (input, id) => {
    const requestCopy = JSON.parse(JSON.stringify(request));
    requestCopy[id] = input;
    setRequest(requestCopy);
  };


  return (
    <div className="container bg-light w-50 p-2">
      <h1 class="text-center">Change Request Details</h1>
      <div className="container align-items-left w-75">
        {!message && (
          <form className="UserForm" onSubmit={putRequest}>
            <div class="row no-gutters w-100">
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
                    value={request.startDate}
                    onChange={(e) => updateProperty(e.target.value, "startDate")}
                    required
                  />
                </Col>
              </Row>
            </div>

            <div class="row no-gutters w-100">
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
                    value={request.endDate}
                    onChange={(e) => updateProperty(e.target.value, "endDate")}
                  />
                </Col>
              </Row>
            </div>


            <div className="buttons">
              <button
                type="submit"
                class="btn btn-primary w-auto m-1"
                disabled={loading}
              >
                Update Vacation Request
              </button>

              <button
                type="button"
                class="btn btn-secondary w-auto m-1"
                onClick={() => navigate("/vacationRequests")}
              >
                Cancel
              </button>
            </div>
          </form>
        )}
        {error && (
          <div class="alert alert-danger" role="alert">
            {error ? error : ""}
          </div>
        )}
        {message && (
          <div class="alert alert-success" role="alert">
            {message ? message : ""}
          </div>
        )}
      </div>
    </div>
  );
};

export default ModifyVacationRequest;
