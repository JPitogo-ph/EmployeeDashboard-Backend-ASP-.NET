using EmployeeDashboard.Data;
using EmployeeDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private AppDbContext _context;
        public EmployeesController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            var employees = await _context.Employees
                            .OrderBy(e => e.EmployeeId)
                            .ToListAsync();
            return Ok(employees);
        }
        
        [HttpGet("{id}")]

        public async Task<ActionResult<Employee>> GetEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetEmployeeAsync), new { id = employee.EmployeeId }, employee);
        }
    }
}
