using EmployeeDashboard.Data;
using EmployeeDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(AppDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            var employees = await dbContext.Employees
                            .AsNoTracking()
                            .OrderBy(e => e.EmployeeId)
                            .ToListAsync();
            return Ok(employees);
        }
        
        [HttpGet("{id}")]

        public async Task<ActionResult<Employee>> GetEmployeeAsync(int id)
        {
            var employee = await dbContext.Employees.AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee is null)
            {
                return NotFound($"Employee with id: {id} was not found");
            }
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployeeAsync(Employee employee)
        {
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetEmployeeAsync), new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAsync(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest($"Input Id {id} does not match {employee.EmployeeId}");
            }
            dbContext.Entry(employee).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
