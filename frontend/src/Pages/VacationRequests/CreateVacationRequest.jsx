import { useState } from "react";
import { useNavigate } from "react-router";
import "./VacationRequest.css";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

const CreateVacationRequest = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");

  const [request, setRequest] = useState({
    employeeId: "",
    startDate: "",
    endDate: ""
  });

  const navigate = useNavigate();

  const postRequest = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage("");
    setError("");

    const url = "/api/VacationRequest";
    const fetchMethod = "POST";
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
    switch (id) {
      case 1:
        requestCopy.employeeId = input;
        break;
      case 2:
        requestCopy.startDate = input;
        break;
      case 3:
        requestCopy.endDate = input;
        break;
      default:
        console.log(" ");
    }

    setRequest(requestCopy);
  };

  return (
    <div className="container bg-light w-50 p-2">
      <h1 className="text-center">Add Request</h1>
      <div className="container align-items-left w-75">
        {!message && (
          <form className="UserForm" onSubmit={postRequest}>
            <div className="row no-gutters w-100">
              <Row>
                <Col>
                  <label htmlFor="employeeId">Employee Id:</label>
                </Col>
                <Col>
                  <input
                    className="w-100"
                    type="int"
                    name="employeeId"
                    id="employeeId"
                    value={request.employeeId}
                    onChange={(e) => updateProperty(e.target.value, 1)}
                    required
                  />
                </Col>
              </Row>
            </div>

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
                    value={request.startDate}
                    onChange={(e) => updateProperty(e.target.value, 2)}
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
                    value={request.endDate}
                    onChange={(e) => updateProperty(e.target.value, 3)}
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
                Create New Vacation Request
              </button>

              <button
                type="button"
                className="btn btn-secondary w-auto m-1"
                onClick={() => navigate("/vacationRequests")}
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

export default CreateVacationRequest;
