import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Roster.css";
import Spinner from "../Layout/Spinner";

const Roster = () => {
  const [rosterList, setRosterList] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");

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
            <th scope="col">Attendance</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody className="p-5">
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
                  </td>
                  <td>{rosterItem.warning}</td>
                  <td>{rosterItem.attendance}</td>
                  <td>
                    <button />
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
