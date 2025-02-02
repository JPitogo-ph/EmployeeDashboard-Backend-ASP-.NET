using EmployeeDashboard.Data;
using EmployeeDashboard.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<ActionResult<Employee>> PostEmployeeAsync([FromBody] Employee employee)
        {
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetEmployeeAsync), new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAsync(int id, [FromBody] Employee? employee)
        {
            if (employee is null)
            {
                return BadRequest("Employee cannot be null");
            }
            
            if (id != employee.EmployeeId)
            {
                return BadRequest($"Input Id {id} does not match {employee.EmployeeId}");
            }
            dbContext.Entry(employee).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployeeAsync(int id, [FromBody] JsonPatchDocument<Employee>? patchDocument) //Right you need parameter binding attrs for complex types
        {
            if (patchDocument is null)
            {
                return BadRequest("Invalid document");
            }
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound($"Employee with id: {id} was not found");
            }
            patchDocument.ApplyTo(employee, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound($"Employee with id: {id} was not found");
            }
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
