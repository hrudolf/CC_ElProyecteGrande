import { useEffect, useState } from "react";
import "./Roster.css";

const Roster = () => {
  const [rosterList, setRosterList] = useState("");
  const [loading, setLoading] = useState(true);
  const [date, setDate] = useState();

  const ChangeAttendance = (rosterId) => {
    fetch(`https://localhost:44353/api/Roster/${rosterId}`, {
      method: "PATCH",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRosterList(json);
      })
      .catch((err) => console.log(err));
  };

  const GenerateWeeklyRoster = (rosterStartDate) => {
    setLoading(true);
    fetch(`https://localhost:44353/api/Roster/GenerateWeeklyRoster`, {
      method: "POST",
      credentials: "include",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(date),
    }).then(() => {
      console.log("Roster created");
    });

    setLoading(true);
    fetch("https://localhost:44353/api/Roster", {
      method: "GET",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRosterList(json);
      })
      .catch((err) => console.log(err));
    window.location.reload(false);
  };

  const DeleteRosterItem = (rosterId) => {
    fetch(`https://localhost:44353/api/Roster/${rosterId}`, {
      method: "DELETE",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRosterList(rosterList.filter((roster) => roster.id !== rosterId));
      })
      .catch((err) => console.log(err));
  };

  useEffect(() => {
    setLoading(true);
    fetch("https://localhost:44353/api/Roster", {
      method: "GET",
      credentials: "include"
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setRosterList(json);
      })
      .catch((err) => console.log(err));
  }, []);

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Roster
      </h1>{" "}
      <div
        className="container align-items-center"
        style={{
          backgroundColor: "rgb(232,232,232)",
          verticalAlign: "middle",
          textAlign: "center",
        }}
      >
        <button
          className="btn btn-warning m-2"
          onClick={(e) => GenerateWeeklyRoster(date)}
        >
          Generate roster
        </button>{" "}
        <input
          className="p-1 m-1"
          style={{ border: "none" }}
          type="date"
          name=""
          id=""
          onChange={(e) => setDate(e.target.value)}
        />
      </div>
      <table className="table table-light table-bordered table-striped table-responsive">
        <thead className="p-2" style={{ verticalAlign: "middle" }}>
          <tr>
            <th scope="col">Date</th>
            <th scope="col">Shift</th>
            <th scope="col">Employee</th>
            <th scope="col">Warning</th>
            <th scope="col" colSpan="2">
              Attendance
            </th>

            <th scope="col"></th>
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
                  <td>{rosterItem.attendance === false ? "none" : "confirmed"}{" "}
                  </td>
                  <td>{" "}
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
          <tr></tr>
        </tbody>
      </table>
    </div>
  );
};

export default Roster;
