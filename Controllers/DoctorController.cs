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
    public class DoctorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var query = await _context.doctors
                       .Include(x=>x.department)
                       .Select(x=>new { x.Name, x.Id, x.Phone, x.Email, x.DateOfBirth,x.departmentId, x.department.Title,x.department.Description })
                       .ToListAsync();

            if (query is null)
                return BadRequest("not exist doctors");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var query = await _context.doctors
                              .Include(x=>x.department)
                              .Select(x => new {x.Name,x.Id,x.Phone,x.Email,x.DateOfBirth,x.departmentId,x.department.Title,x.department.Description })
                              .FirstOrDefaultAsync(x=>x.Id==id);
                                              
            if (query is null)
                return BadRequest("Doctor is not exist");

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> addStudent([FromBody] DoctorDto dto)
        {
            Doctor doctor = new Doctor()
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                Phone = dto.Phone,
                departmentId = dto.departmentId
            };

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (doctor is null)
                return BadRequest("dto is null");

            else
            {
                await _context.doctors.AddAsync(doctor);
                _context.SaveChanges();
            }

            return Created("", doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateDoctor([FromBody] DoctorDto dto, int id)
        {
            var search = await _context.doctors.FindAsync(id);

            if (search is null)
                return BadRequest("doctor is null");

            if (dto is null)
                return BadRequest("dto is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            else
            {
                search.Name = dto.Name;
                search.DateOfBirth = dto.DateOfBirth;
                search.Email = dto.Email;
                search.Phone = dto.Phone;
                search.departmentId = dto.departmentId;

                _context.Update(search);
                _context.SaveChanges();
            }

            return Ok(search);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDoctor(int id)
        {
            var search = await _context.doctors.FindAsync(id);

            if (search is null)
                return BadRequest("doctor is not exist");

            return Ok(search);
        }


    }
}
