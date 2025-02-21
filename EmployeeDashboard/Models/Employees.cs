using System.ComponentModel.DataAnnotations;

namespace EmployeeDashboard.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Employee ID is required")] 
        public int EmployeeId { get; set; }
        
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Employee name must be between 2 and 32 characters")]
        [Required(ErrorMessage = "Employee first name is required")] 
        public required string FirstName { get; set; }
        
        [StringLength(16, MinimumLength = 2, ErrorMessage = "Employee last name must be between 2 and 16 characters")]
        [Required(ErrorMessage = "Employee last name is required")] 
        public required string LastName { get; set; }
        
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Employee email must be between 2 and 32 characters")]
        [Required(ErrorMessage = "Employee email is required")] 
        public required string Email { get; set; }
        
        [StringLength(16, MinimumLength = 4,ErrorMessage = "Employee phone number must be between 4 and 16 characters")]
        public string? PhoneNumber{ get; set; }
        
        [Required(ErrorMessage = "Employee hire date is required")]
        public DateTime HireDate { get; set; }
        
        [StringLength(12, MinimumLength = 4, ErrorMessage = "Employee phone number must be between 4 and 12 characters")]
        [Required(ErrorMessage = "Employee Job Id is required")] 
        public required string JobId { get; set; }
        
        public int Salary {  get; set; }
        
        [Range(0, 100.00)]
        public Decimal? CommissionPct { get; set; }
        
        public int? ManagerId { get; set; }
        
        public int DepartmentId { get; set; }

    }
}
