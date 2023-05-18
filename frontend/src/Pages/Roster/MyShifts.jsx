import { useEffect, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import "./Roster.css";
import Spinner from "../Layout/Spinner";
import { UserContext } from "../../App";

const MyShifts = () => {
  const [rosterList, setRosterList] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const { user } = useContext(UserContext);

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch(`/api/Roster/employee/4`, {
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
        My Shifts
      </h1>{" "}
      <table className="table table-light table-bordered table-striped table-responsive">
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
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default MyShifts;
