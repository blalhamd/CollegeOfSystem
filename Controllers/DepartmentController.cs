using CollegeOfSystem.APPDBCONTEXT;
using CollegeOfSystem.Entites;
using CollegeOfSystem.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeOfSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var query = await _context.departments
                             .Select(x => new {x.Title,x.Description})// .Select(x => new {x.Title,x.Description,x.students,x.doctors})
                             .OrderBy(x => x.Title)
                             .ToListAsync();

            if (query is null)
                return BadRequest("not exist Departments");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var query = await _context.departments
                              .Include(x=>x.doctors)
                              .Include(x=>x.Students)
                              .Select(x => new {x.Id,x.Title,x.Description,x.Students,x.doctors})
                              .FirstOrDefaultAsync(x=>x.Id==id);
                              
            if (query is null)
                return BadRequest("Department is not exist");

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> addDepartment([FromBody] DepartmentDto dto)
        {
            Department department = new Department()
            {
                Title = dto.Title,
                Description= dto.Description
            };

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (department is null)
                return BadRequest("dto is null");

            else
            {
                await _context.departments.AddAsync(department);
                _context.SaveChanges();
            }

            return Created("", department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateDepartment([FromBody] DepartmentDto dto, int id)
        {
            var search = await _context.departments.FindAsync(id);

            if (search is null)
                return BadRequest("not exist departments");

            if (dto is null)
                return BadRequest("dto is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            else
            {
                search.Title = dto.Title;
                search.Description=dto.Description;

                _context.Update(search);
                _context.SaveChanges();
            }

            return Ok(search);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDoctor(int id)
        {
            var search = await _context.departments.FindAsync(id);

            if (search is null)
                return BadRequest("department is not exist");

            return Ok(search);
        }
    }
}
