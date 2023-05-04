import React, { useState, useEffect } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";

function RequestPopUp(props) {
  const [show, setShow] = useState(false);
  const [requestList, setRequestList] = useState("");
  const [vacationDaysUsed, setVacationDaysUsed] = useState(0);
  const [vacationDaysPending, setVacationDaysPending] = useState(0);

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
      .catch((err) => console.log(err));

    fetch(`api/VacationRequest/approved/${props.id}`, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setVacationDaysUsed(json);
      })
      .catch((err) => console.log(err));

    fetch(`api/VacationRequest/pending/${props.id}`, {
      method: "GET",
    })
      .then((res) => res.json())
      .then((json) => {
        setVacationDaysPending(json);
      })
      .catch((err) => console.log(err));
  }, [props.id]);

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
              <p>Vacation days left: {props.vacationDays - vacationDaysUsed}</p>
              <p>Pending vacation days: {vacationDaysPending}</p>
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
