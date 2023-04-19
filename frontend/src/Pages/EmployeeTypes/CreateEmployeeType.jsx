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
      body: JSON.stringify(employeeType),
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
    <div className="TODO">
      <form className="UserForm" onSubmit={postEmployeeType}>
        <div className="control">
          <label htmlFor="employeeType">employeeType:</label>
          <input
            type="employeeType"
            name="employeeType"
            id="employeeType"
            value={employeeType}
            onChange={e => setEmployeeType(e.target.value)}
            required
          />
        </div>

        <div className="buttons">
          <button type="submit" disabled={loading}>
            Create new Employee Type
          </button>

          <button type="button" onClick={() => navigate('/employeetypes')}>
            Cancel
          </button>
        </div>
      </form>
      {error && <div className={"error"}>{error ? error : ""}</div>}
      {message && <div className={"message"}>{message ? message : ""}</div>}
    </div >
  );
};

export default CreateEmployeeType;