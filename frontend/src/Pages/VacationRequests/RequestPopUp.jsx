import React, { useState } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";

function RequestPopUp(props) {
  const [show, setShow] = useState(false);
  const requestList = props.requests.filter(
    (request) => request.employee.id === props.id
  );

  const totalRequestDaysByStatus = (listOfRequests, pendingOrApproved) => {
    return listOfRequests
      .filter((request) => request.isApproved === pendingOrApproved)
      .map((request) => request.noOfDays)
      .reduce((total, amount) => {
        return total + amount;
      }, 0);
  };

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

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
          <table className="table table-striped table-borderless ">
            <thead>
              <tr>
                <th>Start Date</th>
                <th>End Date</th>
                <th>No of Days</th>
                <th>Status</th>
              </tr>
            </thead>
            <tbody>
              {requestList &&
                requestList.map((request) => {
                  return (
                    <tr key={request.id}>
                      <td style={{ width: "100px" }}>
                        {request.startDate.slice(0, 10)}
                      </td>
                      <td style={{ width: "100px" }}>
                        {request.endDate.slice(0, 10)}
                      </td>
                      <td style={{ width: "100px" }}>{request.noOfDays}</td>
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
          <div
            style={{
              display: "flex",
              flexBasis: 0,
              flexGrow: 1,
              flexDirection: "row",
              justifyContent: "space-between",
            }}
          >
            <div
              style={{
                display: "flex",
                flexDirection: "column",
                justifyContent: "left",
                lineHeight: "60%",
              }}
            >
              <p>Total vacation days: {props.vacationDays}</p>
              <p>
                Vacation days left:{" "}
                {props.vacationDays -
                  totalRequestDaysByStatus(requestList, true)}
              </p>
              <p>
                Pending vacation days:{" "}
                {totalRequestDaysByStatus(requestList, false)}
              </p>
            </div>
            <div
              style={{
                display: "flex",
                alignSelf: "flex-end",
              }}
            >
              <Button variant="secondary" onClick={handleClose}>
                Close
              </Button>
            </div>
          </div>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default RequestPopUp;
