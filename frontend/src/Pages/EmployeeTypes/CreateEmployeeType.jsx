import { useState } from "react";
import { useNavigate } from "react-router";
import "./CreateEmployeeType.css";

const CreateEmployeeType = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');

  const [employeeType, setEmployeeType] = useState('');

  const navigate = useNavigate();

  const postEmployeeType = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage('');
    setError('');

    const url = "/api/employeetype";
    const fetchMethod = 'POST';
    const headers = { "Content-Type": "application/json" }

    const response = await fetch(url, {
      method: fetchMethod,
      headers: headers,
      body: JSON.stringify({"type": employeeType}),
    })

    const json = await response.json()
    if (!response.ok) {
      setLoading(false);
      setError(json.error);
    } else {
      setMessage("Employee Type successfully added, you will be redirected.");
      setTimeout(() => navigate("/employeetypes"), 1000);
    }
  };

  return (
    <div class="container bg-light w-25 p-3">
      {!message && 
      <form className="UserForm" onSubmit={postEmployeeType}>
        <label htmlFor="employeeType">Add an employee role:</label>
        <input
          type="text"
          name="employeeType"
          id="employeeType"
          value={employeeType}
          onChange={e => setEmployeeType(e.target.value)}
          required
        />

        <div className="buttons">
          <button type="submit" class="btn btn-primary w-auto m-1" disabled={loading}>
            Create new Employee Type
          </button>

          <button type="button" class="btn btn-secondary w-auto m-1" onClick={() => navigate('/employeetypes')}>
            Cancel
          </button>
        </div>
      </form>
    }
      {error && <div class="alert alert-danger" role="alert">{error ? error : ""}</div>}
      {message && <div class="alert alert-success" role="alert">{message ? message : ""}</div>}
    </div >
  );
};

export default CreateEmployeeType;