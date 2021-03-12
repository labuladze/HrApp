using HrApi.Data;
using HrApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApi.Controllers
{   
    [Route("api/employee")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        //Get : api/employee
        public async Task<IActionResult> GetEmpl()
        {
            return Ok( await _context.Employees.ToListAsync());
        }


        //Get : api/employee/id
        [HttpGet("{id}",Name = "GetEmplById")]
        public async Task<IActionResult> GetEmplById(int id)
        {
            var res =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound();
        }


        //Post : api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmpl(Employee empl)
        {
            empl.Id = 0;
            await _context.Employees.AddAsync(empl);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(nameof(GetEmplById), new { id = empl.Id}, empl);
        }


        //Put : api/employee/id
        [HttpPut("{id}")]
        public  async Task<IActionResult> UpdateEmpl(int id, Employee empl)
        {
            var res = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (res !=null)
            {
                _context.ChangeTracker.Clear();
                empl.Id = id;
                _context.Employees.Update(empl);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }


        //Delete : api/employee/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (res!= null)
            {
                _context.Employees.Remove(res);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

    }
}
