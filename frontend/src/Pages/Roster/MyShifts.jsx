import { useEffect, useState, useContext } from "react";
import "./Roster.css";

import { UserContext } from "../../App";

const MyShifts = () => {
  const [rosterList, setRosterList] = useState(null);

  const { user } = useContext(UserContext);

  useEffect(() => {
    fetch(`https://localhost:7124/api/Roster/employee/${user.id}`, {
      method: "GET",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        console.log(json);
        setRosterList(json);
      })
      .catch((err) => console.log(err));
  }, [user.id]);

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        My Shifts
      </h1>{" "}
      <table
        className="table table-light table-bordered table-striped table-responsive"
        style={{ maxWidth: "500px" }}
      >
        <thead className="p-2" style={{ verticalAlign: "middle" }}>
          <tr>
            <th scope="col">Date</th>
            <th scope="col">Shift</th>
            <th scope="col">Name</th>
          </tr>
        </thead>
        <tbody className="p-5" style={{ verticalAlign: "middle" }}>
          {rosterList &&
            rosterList.map((rosterItem) => {
              return (
                <tr key={rosterItem.id}>
                  <td>{rosterItem.date.slice(0, 10)}</td>
                  <td>{rosterItem.shift.nameOfShift}</td>
                  <td>
                    {rosterItem.employee == null
                      ? ""
                      : rosterItem.employee.firstName +
                        " " +
                        rosterItem.employee.lastName}
                  </td>
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default MyShifts;
