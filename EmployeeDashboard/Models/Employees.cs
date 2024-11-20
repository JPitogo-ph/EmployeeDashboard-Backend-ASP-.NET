namespace EmployeeDashboard.Models
{
    public class Employees
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber{ get; set; }
        public DateTime? HireDate { get; set; }
        public string? JobId { get; set; }
        public int Salary {  get; set; }
        public Decimal CommissionPct { get; set; }
        public int ManagerId { get; set; }
        public int DepartmentId { get; set; }

    }
}
