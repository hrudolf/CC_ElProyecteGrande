import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CreateEmployeeType.css";

const EmployeeTypes = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [message, setMessage] = useState('');
    const [employeeTypeList, setEmployeeTypeList] = useState('');

    const navigate = useNavigate();

    useEffect(() => {
        setLoading(true);
        setMessage('');
        setError('');
        fetch("/api/employeetype", {
            method: "GET"
        })
            .then(res => res.json())
            .then(json => {
                setLoading(false);
                console.log("Loading set to false");
                setEmployeeTypeList(json);
            })
            .catch(err => setError(err))
    }, [])

    return (
        <div className="EmployeeTypes">
            <h1>EmployeeTypes</h1>
            <div className="TODO">
                {employeeTypeList && <div className="employeeTypes">
                    {employeeTypeList.map(employeeType => <p>Id: {employeeType.id}, {employeeType.type}</p>)}
                </div>}
                {loading && <div className={"loading"}>Loading...</div>}
                {error && <div className={"error"}>{error ? error : ""}</div>}
                {message && <div className={"message"}>{message ? message : ""}</div>}
                <button onClick={() => navigate("/employeetypes/create")}>Add new type</button>
            </div >
        </div>
    );
}

export default EmployeeTypes;