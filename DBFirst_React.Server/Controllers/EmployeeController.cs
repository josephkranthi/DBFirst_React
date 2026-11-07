using DBFirst_React.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBFirst_React.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();

            return Ok(employees);
        }

        // GET: api/Employee/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        // PUT: api/Employee/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            _context.Entry(employee).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        // DELETE: api/Employee/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}