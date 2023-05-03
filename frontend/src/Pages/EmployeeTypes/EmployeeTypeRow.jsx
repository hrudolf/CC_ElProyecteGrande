import { useState } from "react";

const EmployeeTypeRow = ({ employeeType, DeleteEmployee, loading, HandleUpdate }) => {
    const [editing, setEditing] = useState(false);
    const [input, setInput] = useState(employeeType.type);

    const handleEnter = (e) => {
        if (e.key === 'Enter') {
            setEditing(false);
            handleDataUpdate();
        }
    }

    const handleDataUpdate = () => {
        if (input !== employeeType.type) {
            HandleUpdate(employeeType.id, input);
        }
    }

    return (
        <tr>
            <th scope="row">{employeeType.id}</th>
            {!editing && <td className="sanyi" onClick={() => setEditing(true)}>{input}</td>}
            {editing && <td> <input autoFocus type="text" value={input} onKeyDown={handleEnter} onChange={e => setInput(e.target.value)} /> <span onClick={(e) => {setEditing(false); handleDataUpdate();}}>✔️</span></td>}
            <td> <button class="btn btn-warning" onClick={() => DeleteEmployee(employeeType.id)} disabled={loading}>Delete</button> </td>
        </tr>
    );
}

export default EmployeeTypeRow;