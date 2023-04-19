import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CreateEmployeeType.css";

const EmployeeTypes = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [message, setMessage] = useState('');
    const [employeeTypeList, setEmployeeTypeList] = useState('');

    const navigate = useNavigate();

    const DeleteEmployee = (employeeTypeId) => {
        fetch(`/api/employeetype/${employeeTypeId}`, {
            method: "DELETE"
        })
            .then(res => res.json())
            .then(json => {
                setLoading(false);
                setEmployeeTypeList(employeeTypeList.filter(employeeType => employeeType.id != employeeTypeId));
            })
            .catch(err => setError(err))
    }

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
                setEmployeeTypeList(json);
            })
            .catch(err => setError(err))
    }, [])

    return (
        <div className="EmployeeTypes">
            <h1>Employee Roles</h1>
            <div className="TODO">
                {employeeTypeList && <div className="employeeTypes">
                    <table>
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Role name</th>
                            </tr>
                        </thead>
                        <tbody>
                            {employeeTypeList.map(employeeType => {
                                return (
                                    <tr>
                                        <td>{employeeType.id}</td>
                                        <td>{employeeType.type}</td>
                                        <td> <button onClick={() => navigate(`/employeetypes/edit/${employeeType.id}`)} disabled={loading}>Edit</button> </td>
                                        <td> <button onClick={() => DeleteEmployee(employeeType.id)} disabled={loading}>Delete</button> </td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>}
                {loading && <div className={"loading"}>Loading...</div>}
                {error && <div className={"error"}>{error ? error : ""}</div>}
                {message && <div className={"message"}>{message ? message : ""}</div>}
                <button onClick={() => navigate("/employeetypes/create")}>Add a new role</button>
            </div >
        </div>
    );
}

export default EmployeeTypes;