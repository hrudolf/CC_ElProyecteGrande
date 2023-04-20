import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import "./CreateEmployeeType.css";

const ModifyEmployeeType = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const { id } = useParams();

  const [employeeType, setEmployeeType] = useState('');

  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    setMessage('');
    setError('');
    fetch(`/api/EmployeeType/${id}`, {
        method: "GET"
    })
        .then(res => res.json())
        .then(json => {
            setLoading(false);
            setEmployeeType(json.type);
        })
        .catch(err => setError(err))
}, [id])

  const updateEmployeeType = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage('');
    setError('');

    const url = `/api/employeetype/`;
    const fetchMethod = 'PUT';
    const headers = { "Content-Type": "application/json" }
    const body = {"id": id, "type": employeeType}

    console.log(employeeType);

    const response = await fetch(url, {
      method: fetchMethod,
      headers: headers,
      body: JSON.stringify(body),
    })

    const json = await response.json()
    if (!response.ok) {
      setLoading(false);
      setError(json.error);
    } else {
      setMessage("Employee Type successfully updated, you will be redirected.");
      setTimeout(() => navigate("/employeetypes"), 1000);
    }
  };

  return (
    <div class="container bg-light w-25 p-3">
      <form className="UserForm" onSubmit={updateEmployeeType}>
        <label htmlFor="employeeType">Edit employee role name:</label>
        <input
          type="employeeType"
          name="employeeType"
          id="employeeType"
          value={employeeType}
          onChange={e => setEmployeeType(e.target.value)}
          required
        />

        <div className="buttons">
          <button type="submit" class="btn btn-primary w-auto m-1" disabled={loading}>
            Update Employee Role Name
          </button>

          <button type="button" class="btn btn-secondary w-auto m-1" onClick={() => navigate('/employeetypes')}>
            Cancel
          </button>
        </div>
      </form>
      {error && <div className={"error"}>{error ? error : ""}</div>}
      {message && <div className={"message"}>{message ? message : ""}</div>}
    </div >
  );
};

export default ModifyEmployeeType;