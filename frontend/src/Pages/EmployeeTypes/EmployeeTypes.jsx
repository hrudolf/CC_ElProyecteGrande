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
                setEmployeeTypeList(employeeTypeList.filter(employeeType => employeeType.id !== employeeTypeId));
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
        <div class="container align-items-center">
            <h1 class="p-2 m-2">Employee Roles</h1>
            <div class="container align-items-center">
                {employeeTypeList && <div className="employeeTypes">
                    <table class="table table-bordered table-striped table-responsive">
                        <thead class="p-2">
                            <tr>
                                <th scope="col">Id</th>
                                <th scope="col">Role name</th>
                            </tr>
                        </thead>
                        <tbody class="p-5">
                            {employeeTypeList.map(employeeType => {
                                return (
                                    <tr>    
                                        <th scope="row">{employeeType.id}</th>
                                        <td>{employeeType.type}</td>
                                        <td> <button class="btn btn-secondary" onClick={() => navigate(`/employeetypes/edit/${employeeType.id}`)} disabled={loading}>Edit</button> </td>
                                        <td> <button class="btn btn-warning" onClick={() => DeleteEmployee(employeeType.id)} disabled={loading}>Delete</button> </td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>}
                {loading && <div className={"loading"}>Loading...</div>}
                {error && <div className={"error"}>{error ? error : ""}</div>}
                {message && <div className={"message"}>{message ? message : ""}</div>}
                <button class="btn btn-primary w-auto" onClick={() => navigate("/employeetypes/create")}>Add a new role</button>
            </div >
        </div>
    );
}

export default EmployeeTypes;