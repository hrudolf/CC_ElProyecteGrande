import { useState } from "react";

const EmployeeTypeNewRow = ({ employeeType, loading, HandlePost, setNewRow }) => {
    const [editing, setEditing] = useState(true);
    const [input, setInput] = useState(employeeType.type);

    const handleEnter = (e) => {
        if (e.key === 'Enter') {
            setEditing(false);
            handleDataUpdate();
        }
    }

    const handleDataUpdate = () => {
        if (input !== employeeType.type) {
            HandlePost(input);
        }
    }

    return (
        <tr>
            <th scope="row">{employeeType.id}</th>
            {!editing && <td onClick={() => setEditing(true)}>{input}</td>}
            {editing && <td> <input autoFocus type="text" value={input} onKeyDown={handleEnter} onChange={e => setInput(e.target.value)} /> <span onClick={(e) => {setEditing(false); handleDataUpdate();}}>✔️</span></td>}
            <td> <button class="btn btn-warning" onClick={() => setNewRow(false)} disabled={loading}>Cancel</button> </td>
        </tr>
    );
}

export default EmployeeTypeNewRow;