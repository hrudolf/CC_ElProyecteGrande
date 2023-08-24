import { useEffect, useState } from "react";
import Spinner from "../Layout/Spinner";
import "./Forecast.css";

const Forecast = () => {
  const [forecastList, setForecastList] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    fetch("https://localhost:7124/api/Roster/forecast", {
      method: "GET",
      credentials: "include",
    })
      .then((res) => res.json())
      .then((json) => {
        setLoading(false);
        setForecastList(json);
      })
      .catch((err) => console.log(err));
  }, []);

  return (
    <div className="container align-items-center">
      <h1
        style={{ backgroundColor: "rgb(255, 255, 255,0.7)" }}
        className="p-2 m-2"
      >
        Weekly Salary Forecast
      </h1>{" "}
      {forecastList && (
        <table
          className="table table-light table-bordered table-striped table-responsive"
          style={{ width: "600px" }}
        >
          <thead className="p-2" style={{ verticalAlign: "middle" }}>
            <tr>
              <th scope="col">Start of Week</th>
              <th scope="col">End of Week</th>
              <th scope="col">Salary Forecast </th>
            </tr>
          </thead>
          <tbody className="p-5" style={{ verticalAlign: "middle" }}>
            {forecastList &&
              forecastList.map((item) => {
                return (
                  <tr>
                    <td>{item.startOfWeek.slice(0, 10)}</td>
                    <td>{item.endOfWeek.slice(0, 10)}</td>
                    <td>
                      $
                      {parseInt(item.amount).toLocaleString("en-US", {
                        valute: "USD",
                      })}
                    </td>
                  </tr>
                );
              })}
          </tbody>
        </table>
      )}
      {loading && (
        <div>
          <Spinner />
        </div>
      )}
    </div>
  );
};

export default Forecast;
