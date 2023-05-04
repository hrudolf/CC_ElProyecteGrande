import { useState } from "react";

const ShiftNewRow = ({ shift, loading, HandlePost, setNewRow }) => {
    const [edit, setEdit] = useState([true, true, true]);
    const [shiftObject, setShiftObject] = useState(shift);
    const shiftDto = JSON.parse(JSON.stringify(shift));

    const modifyEdit = (num) => {
        const editCopy = JSON.parse(JSON.stringify(edit));
        editCopy[num] = !editCopy[num];
        setEdit(editCopy);
    }

    const updateProperty = (input, key) => {
        const shiftCopy = JSON.parse(JSON.stringify(shiftObject));
        if (shiftCopy[key] !== input) {
            shiftCopy[key] = input;
            setShiftObject(shiftCopy);
        }
    };

    const handleKeys = (pressedKey, objKey, num) => {
        if (pressedKey === 'Enter') {
            modifyEdit(num);
            shiftDto.nameOfShift = shiftObject.nameOfShift;
            shiftDto.nursesRequiredForShift = +shiftObject.nursesRequiredForShift;
            shiftDto.bonusRate = +shiftObject.bonusRate;
            
            if (shiftDto.nameOfShift === "") return;
            if (!isEqual(shiftDto, shift)) {
                HandlePost(shiftDto);
            }
        }
        if (pressedKey === 'Escape') {
            setNewRow(false);
        }
    }

    const isEqual = (obj1, obj2) => {
        return JSON.stringify(obj1) === JSON.stringify(obj2);
    }

    return (
        <tr>
            <th scope="row">{shift.id}</th>
            {!edit[0] && <td className="datarow" onClick={() => { if (!loading) modifyEdit(0) }}>{shiftObject.nameOfShift}</td>}
            {edit[0] && <td className="datarow"> <input autoFocus type="text" value={shiftObject.nameOfShift} onKeyDown={(e) => handleKeys(e.key, "nameOfShift", 0)} onChange={e => updateProperty(e.target.value, "nameOfShift")} /> <span onClick={() => handleKeys('Enter', "nameOfShift", 0)}>✔️</span></td>}

            {!edit[1] && <td className="datarow" onClick={() => { if (!loading) modifyEdit(1) }}>{shiftObject.nursesRequiredForShift}</td>}
            {edit[1] && <td className="datarow"> <input type="number" step="1" min="0" value={shiftObject.nursesRequiredForShift} onKeyDown={(e) => handleKeys(e.key, "nursesRequiredForShift", 1)} onChange={e => updateProperty(e.target.value, "nursesRequiredForShift")} /> <span onClick={() => handleKeys('Enter', "nursesRequiredForShift", 1)}>✔️</span></td>}

            {!edit[2] && <td className="datarow" onClick={() => { if (!loading) modifyEdit(2) }}>{shiftObject.bonusRate}</td>}
            {edit[2] && <td className="datarow"> <input type="number" step="0.1" min="0" value={shiftObject.bonusRate} onKeyDown={(e) => handleKeys(e.key, "bonusRate", 2)} onChange={e => updateProperty(e.target.value, "bonusRate")} /> <span onClick={() => handleKeys('Enter', "bonusRate", 2)}>✔️</span></td>}

            <td><button className="btn btn-warning" onClick={() => setNewRow(false)} disabled={loading}>Cancel</button> </td>
        </tr>
    );
}


export default ShiftNewRow;