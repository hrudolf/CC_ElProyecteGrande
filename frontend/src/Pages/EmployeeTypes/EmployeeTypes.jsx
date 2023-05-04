import { useEffect, useState } from "react";
import "./CreateEmployeeType.css";
import EmployeeTypeRow from "./EmployeeTypeRow";
import EmployeeTypeNewRow from "./EmployeeTypeNewRow";
import Spinner from "../Layout/Spinner";

const EmployeeTypes = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [employeeTypeList, setEmployeeTypeList] = useState("");
  const [newRow, setNewRow] = useState(false);

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch("/api/employeetype", {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeTypeList(json);
      })
      .catch((err) => setError(err));
  }, []);

  const DeleteEmployee = (employeeTypeId) => {
    fetch(`/api/employeetype/${employeeTypeId}`, {
      method: "DELETE",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setEmployeeTypeList(
          employeeTypeList.filter(
            (employeeType) => employeeType.id !== employeeTypeId
          )
        );
      })
      .catch((err) => setError(err));
  };

  const newEmployee = {
    id: "",
    type: "",
  };

  const HandleUpdate = async (id, input) => {
    setLoading(true);
    const response = await fetch(`/api/employeetype/`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ id: id, type: input }),
    });

    const json = await response.json();
    if (response.ok) {
      setLoading(false);
      const employeeTypeListCopy = JSON.parse(JSON.stringify(employeeTypeList));
      const updatedIndex = employeeTypeListCopy.findIndex(
        (elem) => elem.id === json.id
      );
      employeeTypeListCopy[updatedIndex].type = json.type;
      setEmployeeTypeList(employeeTypeListCopy);
    }
  };

  const HandlePost = async (input) => {
    setLoading(true);
    const response = await fetch(`/api/employeetype/`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ type: input }),
    });

    const json = await response.json();
    if (response.ok) {
      setLoading(false);
      const employeeTypeListCopy = JSON.parse(JSON.stringify(employeeTypeList));
      employeeTypeListCopy.push(json);
      setEmployeeTypeList(employeeTypeListCopy);
      setNewRow(false);
    } else {
      setError(
        "Oops, something happened, please refresh the page and try again later."
      );
    }
  };

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Employee Roles
      </h1>
      <div className="container align-items-center">
        {employeeTypeList && (
          <div className="employeeTypes">
            <table className="table table-light table-bordered table-striped table-responsive">
              <thead>
                <tr>
                  <th scope="col">Id</th>
                  <th scope="col">Role name</th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody>
                {employeeTypeList.map((employeeType) => (
                  <EmployeeTypeRow
                    key={employeeType.id}
                    employeeType={employeeType}
                    DeleteEmployee={DeleteEmployee}
                    loading={loading}
                    HandleUpdate={HandleUpdate}
                  ></EmployeeTypeRow>
                ))}
                {newRow && (
                  <EmployeeTypeNewRow
                    employeeType={newEmployee}
                    loading={loading}
                    HandlePost={HandlePost}
                    setNewRow={setNewRow}
                  />
                )}
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
        {!loading && (
          <button
            className="btn btn-primary w-auto"
            onClick={() => setNewRow(true)}
          >
            Add a new role
          </button>
        )}
      </div>
    </div>
  );
};

export default EmployeeTypes;
