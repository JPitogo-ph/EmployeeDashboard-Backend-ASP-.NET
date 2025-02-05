using System.ComponentModel.DataAnnotations;

namespace EmployeeDashboard.Models
{
    public class Employee
    {
        [Required] 
        public int EmployeeId { get; set; }
        
        [StringLength(32)]
        [Required] 
        public required string FirstName { get; set; }
        
        [StringLength(16)]
        [Required] 
        public required string LastName { get; set; }
        
        [StringLength(32)]
        [Required] 
        public required string Email { get; set; }
        
        [StringLength(16)]
        [Required] 
        public required string PhoneNumber{ get; set; }
        
        public DateTime? HireDate { get; set; }
        
        [StringLength(8)]
        [Required] 
        public required string JobId { get; set; }
        
        public int Salary {  get; set; }
        
        public Decimal CommissionPct { get; set; }
        
        public int ManagerId { get; set; }
        
        public int DepartmentId { get; set; }

    }
}
