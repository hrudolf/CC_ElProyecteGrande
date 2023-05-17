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
    </div>
  );
};

export default Roster;
