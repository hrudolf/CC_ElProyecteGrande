import { useState } from "react";

const EmployeeTypeNewRow = ({ employeeType, loading, HandlePost, setNewRow }) => {
    const [editing, setEditing] = useState(true);
    const [input, setInput] = useState(employeeType.type);

    const handleKeys = (e) => {
        console.log(e.key);
        if (e.key === 'Enter') {
            handleDataUpdate();
        }
        if (e.key === 'Escape') {
            setNewRow(false);
        }
    }

    const handleDataUpdate = () => {
        setEditing(false);
        if (input !== employeeType.type) {
            HandlePost(input);
        } else {
            setNewRow(false);
        }
    }

    return (
        <tr>
            <th scope="row">{employeeType.id}</th>
            {!editing && <td> <span onClick={() => { if (!loading) setEditing(true) }}>{input} </span></td>}
            {editing && <td> <input autoFocus type="text" value={input} onKeyDown={handleKeys} onChange={e => setInput(e.target.value)} /> <span onClick={handleDataUpdate}>✔️</span></td>}
            <td> <button className="btn btn-warning" onClick={() => setNewRow(false)} disabled={loading}>Cancel</button> </td>
        </tr>
    );
}

export default EmployeeTypeNewRow;