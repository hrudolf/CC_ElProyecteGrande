using System.ComponentModel.DataAnnotations.Schema;
using backend.DTOs;
using System.Text.Json.Serialization;


namespace backend.Model
{
    public class VacationRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public int NoOfDays
        {
            get { return (this.EndDate - this.StartDate).Days; }
        }
        public bool IsApproved { get; private set; }

        /*public VacationRequest(Employee employee, DateTime startDate, DateTime endDate)
        {
            Employee = employee;
            StartDate = startDate;
            EndDate = endDate;
            IsApproved = false;
        }
        
        [JsonConstructor]
        public VacationRequest(int requestId, Employee employee, DateTime startDate, DateTime endDate, bool isApproved = false)
        {
            Id = requestId;
            Employee = employee;
            StartDate = startDate;
            EndDate = endDate;
            IsApproved = isApproved;
        }*/
    
        public void UpdateVacationRequest(VacationRequest updatedData)
        {
            StartDate = updatedData.StartDate;
            EndDate = updatedData.EndDate;
            IsApproved = updatedData.IsApproved;
        }
        public void ChangeApproval(bool approvalState)
        {
            IsApproved = approvalState;
        }
    }
}