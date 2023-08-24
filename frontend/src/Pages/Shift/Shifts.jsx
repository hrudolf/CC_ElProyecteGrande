import { useEffect, useState } from "react";
import "./Shifts.css";
import Spinner from "../Layout/Spinner";
import ShiftNewRow from "./ShiftNewRow";
import ShiftRow from "./ShiftRow";

const Shifts = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const [shiftList, setShiftList] = useState("");
  const [newRow, setNewRow] = useState(false);

  useEffect(() => {
    setLoading(true);
    setMessage("");
    setError("");
    fetch("https://localhost:7124/api/Shift", {
      method: "GET",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setShiftList(json);
      })
      .catch((err) => setError(err));
  }, []);

  const DeleteShift = (shiftId) => {
    fetch(`https://localhost:7124/api/Shift/${shiftId}`, {
      method: "DELETE",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setShiftList(shiftList.filter((shift) => shift.id !== shiftId));
      })
      .catch((err) => setError(err));
  };

  const newShift = {
    nameOfShift: "",
    nursesRequiredForShift: 0,
    bonusRate: 1,
  };

  const HandleUpdate = async (shift) => {
    setLoading(true);
    console.log("HandleUpdate:");
    console.log(shift);
    const response = await fetch(`https://localhost:7124/api/Shift/`, {
      method: "PUT",
      credentials: "include",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        id: shift.id,
        nameOfShift: shift.nameOfShift,
        nursesRequiredForShift: shift.nursesRequiredForShift,
        bonusRate: shift.bonusRate,
      }),
    });

    const json = await response.json();
    if (response.ok) {
      setLoading(false);
      const shiftListCopy = JSON.parse(JSON.stringify(shiftList));
      const updatedIndex = shiftListCopy.findIndex(
        (elem) => elem.id === json.id
      );
      shiftListCopy[updatedIndex] = json;
      setShiftList(shiftListCopy);
    }
  };

  const HandlePost = async (shift) => {
    setLoading(true);
    const response = await fetch(`https://localhost:7124/api/Shift/`, {
      method: "POST",
      credentials: "include",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nameOfShift: shift.nameOfShift,
        nursesRequiredForShift: shift.nursesRequiredForShift,
        bonusRate: shift.bonusRate,
      }),
    });

    if (!response.ok) {
      setLoading(false);
      setError(
        "Oops, something happened, please refresh the page and try again later."
      );
      setNewRow(false);
    } else {
      const json = await response.json();
      setLoading(false);
      const shiftListCopy = JSON.parse(JSON.stringify(shiftList));
      shiftListCopy.push(json);
      setShiftList(shiftListCopy);
      setNewRow(false);
    }
  };

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Shifts
      </h1>
      <div className="container align-items-center">
        {shiftList && (
          <div className="employeeTypes">
            <table className="table table-light table-bordered table-striped table-responsive">
              <thead>
                <tr>
                  <th scope="col">Id</th>
                  <th scope="col">Shift name</th>
                  <th scope="col">Required nurses for shift</th>
                  <th scope="col">Bonus rate</th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody>
                {shiftList.map((shift) => (
                  <ShiftRow
                    key={shift.id}
                    shift={shift}
                    DeleteShift={DeleteShift}
                    loading={loading}
                    HandleUpdate={HandleUpdate}
                  />
                ))}
                {newRow && (
                  <ShiftNewRow
                    shift={newShift}
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
            Add a new shift
          </button>
        )}
      </div>
    </div>
  );
};

export default Shifts;
