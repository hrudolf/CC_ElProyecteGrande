import React, { useState, useEffect } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";

function RequestPopUp(props) {
  const [show, setShow] = useState(false);
  const [requestList, setRequestList] = useState("");
  const [error, setError] = useState("");

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  useEffect(() => {
    fetch(`api/VacationRequest/employee/${props.id}`, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setRequestList(json);
      })
      .catch((err) => setError(err));
  }, []);

  return (
    <>
      <Button variant="primary" onClick={handleShow}>
        Vacation Requests
      </Button>

      <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          <Modal.Title>
            {props.firstName} {props.lastName} - Vacation Requests
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <table class="table-auto">
            <thead>
              <tr>
                <th>Start Date</th>
                <th>End Date</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {requestList &&
                requestList.map((request) => {
                  return (
                    <tr>
                      <td style={{ width: "100px" }}>
                        {request.startDate.slice(0, 10)}
                      </td>
                      <td style={{ width: "100px" }}>
                        {request.endDate.slice(0, 10)}
                      </td>
                      <td style={{ width: "100px" }}>
                        {request.isApproved ? "approved" : "pending"}
                      </td>
                    </tr>
                  );
                })}
            </tbody>
          </table>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default RequestPopUp;
