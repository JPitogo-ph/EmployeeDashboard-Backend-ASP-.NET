﻿using EmployeeDashboard.Data;
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
            var doesEmployeeExist = dbContext.Employees.Any(e => e.EmployeeId == employee.EmployeeId);
            if (doesEmployeeExist)
            {
                return Conflict($"Employee with id: {employee.EmployeeId} already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
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
                return Conflict($"Input Id {employee.EmployeeId} does not match existing id: {id}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            dbContext.Entry(employee).State = EntityState.Modified;
            //Manually change state, assumes all fields are updated
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        //I've been informed no one actually uses this, comment it out without deleting so I can reference it in the future.
        // [HttpPatch("{id}")]
        // public async Task<IActionResult> PatchEmployeeAsync(int id, [FromBody] JsonPatchDocument<Employee>? patchDocument) //Right you need parameter binding attrs for complex types
        // {
        //     if (patchDocument is null)
        //     {
        //         return BadRequest("Invalid document");
        //     }
        //     var employee = await dbContext.Employees.FindAsync(id);
        //     if (employee is null)
        //     {
        //         return NotFound($"Employee with id: {id} was not found");
        //     }
        //     patchDocument.ApplyTo(employee, ModelState);
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     await dbContext.SaveChangesAsync();
        //     
        //     return NoContent();
        // }

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
