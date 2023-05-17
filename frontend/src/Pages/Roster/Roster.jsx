import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Roster.css";
import Spinner from "../Layout/Spinner";

const Roster = () => {
  const [rosterList, setRosterList] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");

  const ChangeAttendance = (rosterId) => {
    fetch(`/api/Roster/${rosterId}`, {
      method: "PATCH",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRosterList(json);
      })
      .catch((err) => setError(err));
  };

  const DeleteRosterItem = (rosterId) => {
    fetch(`/api/Roster/${rosterId}`, {
      method: "DELETE",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRosterList(rosterList.filter((roster) => roster.id !== rosterId));
      })
      .catch((err) => setError(err));
    /*  window.location.reload(false); */
  };

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch("/api/Roster", {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRosterList(json);
      })
      .catch((err) => setError(err));
  }, []);

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Roster
      </h1>
      <table className="table table-light table-bordered table-striped table-responsive">
        <thead className="p-2" style={{ verticalAlign: "middle" }}>
          <tr>
            <th scope="col">Date</th>
            <th scope="col">Shift</th>
            <th scope="col">Employee</th>
            <th scope="col">Warning</th>
            <th scope="col" colspan="2">
              Attendance
            </th>

            <th scope="col"></th>
          </tr>
        </thead>
        <tbody className="p-5" style={{ verticalAlign: "middle" }}>
          {rosterList &&
            rosterList.map((rosterItem) => {
              return (
                <tr>
                  <td>{rosterItem.date.slice(0, 10)}</td>
                  <td>{rosterItem.shift.nameOfShift}</td>
                  <td>
                    {rosterItem.employee == null
                      ? ""
                      : rosterItem.employee.firstName +
                        " " +
                        rosterItem.employee.lastName}
                    {rosterItem.employee == null
                      ? ""
                      : rosterItem.employee.employeeType.type ===
                        "Shift lead nurse"
                      ? " (Shift lead)"
                      : ""}
                  </td>
                  <td className="text-danger fw-bold">{rosterItem.warning}</td>
                  <td>
                    {rosterItem.attendance === false ? "none" : "confirmed"}{" "}
                  </td>
                  <td>
                    {" "}
                    <button
                      className="btn btn-info"
                      onClick={() => ChangeAttendance(rosterItem.id)}
                      disabled={loading}
                    >
                      Confirm
                    </button>{" "}
                  </td>
                  <td>
                    <button className="btn btn-secondary" disabled={loading}>
                      Edit
                    </button>{" "}
                    <button
                      className="btn btn-warning"
                      onClick={() => DeleteRosterItem(rosterItem.id)}
                      disabled={loading}
                    >
                      Delete
                    </button>{" "}
                  </td>
                </tr>
              );
            })}
          ;
        </tbody>
      </table>
    </div>
  );
};

export default Roster;
