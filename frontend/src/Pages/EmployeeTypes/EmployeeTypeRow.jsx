import { useState } from "react";

const EmployeeTypeRow = ({ employeeType, DeleteEmployee, loading, HandleUpdate }) => {
    const [editing, setEditing] = useState(false);
    const [input, setInput] = useState(employeeType.type);

    const handleKeys = (e) => {
        if (e.key === 'Enter') {
            handleDataUpdate();
        }
        if (e.key === 'Escape') {
            setEditing(false);
            setInput(employeeType.type);
        }
    }

    const handleDataUpdate = () => {
        setEditing(false);
        if (input !== employeeType.type) {
            HandleUpdate(employeeType.id, input);
        }
    }

    return (
        <tr>
            <th scope="row">{employeeType.id}</th>
            {!editing && <td className="datarow" onClick={() => { if (!loading) setEditing(true) }}>{input}</td>}
            {editing && <td className="datarow"> <input autoFocus type="text" value={input} onKeyDown={handleKeys} onChange={e => setInput(e.target.value)} /> <span onClick={handleDataUpdate}>✔️</span></td>}
            <td> <button class="btn btn-warning" onClick={() => DeleteEmployee(employeeType.id)} disabled={loading}>Delete</button> </td>
        </tr>
    );
}

export default EmployeeTypeRow;