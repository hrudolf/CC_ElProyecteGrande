import { useEffect, useState } from "react";
import "./Roster.css";

const RosterPublic = () => {
  const [rosterList, setRosterList] = useState("");

  useEffect(() => {
    fetch(process.env.REACT_APP_APIURL + "/api/Roster", {
      method: "GET",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        setRosterList(json);
      })
      .catch((err) => alert(err));
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
          </tr>
        </thead>
        <tbody className="p-5" style={{ verticalAlign: "middle" }}>
          {rosterList &&
            rosterList.map((rosterItem) => {
              return (
                <tr key={rosterItem.id}>
                  <td>{rosterItem.date.slice(0, 10)}</td>
                  <td>{rosterItem.shift.nameOfShift}</td>
                  <td>{rosterItem.employee == null
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
                </tr>
              );
            })}
          <tr></tr>
        </tbody>
      </table>
    </div>
  );
};

export default RosterPublic;
