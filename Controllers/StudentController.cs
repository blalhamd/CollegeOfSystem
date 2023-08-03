using CollegeOfSystem.APPDBCONTEXT;
using CollegeOfSystem.Entites;
using CollegeOfSystem.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeOfSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private long AllowLengthForFile = 1048576;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var query=await _context.students.Include(x=>x.department)
                            .Select(x=>new {x.Name,x.Id,x.Email,x.Phone,x.BirthDay,x.departmentId,x.department.Title,x.department.Description})
                            .OrderBy(x=>x.Name)
                            .ToListAsync();

            if (query is null)
                return BadRequest("not exist students");

            return Ok(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var query = await _context.students
                        .Include(x => x.department)
                        .Select(x => new { x.Name, x.Id, x.Phone, x.BirthDay, x.Email, x.departmentId, x.department.Title, x.department.Description })
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (query is null)
                return BadRequest("student is not exist");

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> addStudent([FromForm] StudentDto dto)
        {
            using var dataStream=new MemoryStream();
            await dto.Form.CopyToAsync(dataStream);

            Student student = new Student()
            {
                Name = dto.Name,
                BirthDay = dto.BirthDay,
                Email = dto.Email,
                Phone = dto.Phone,
                Form = dataStream.ToArray(),
                departmentId = dto.departmentId

            };

            if (student.Form.Length > AllowLengthForFile)
                return BadRequest("length of file is beigger than 1M");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            if (student is null)
                return BadRequest("dto is null");

            else
            {
                await _context.students.AddAsync(student);
                _context.SaveChanges();
            }

            return Created("", student);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> updateStudent([FromForm] StudentDto dto,int id)
        {
            var search = await _context.students.FindAsync(id);

            if (dto.Form.Length > AllowLengthForFile)
                return BadRequest("length of file is beigger than 1M");

            if (search is null)
                return BadRequest("student is null");

            if (dto is null)
                return BadRequest("dto is null");

            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");

            else
            {
                using var dataStream = new MemoryStream();
                await dto.Form.CopyToAsync(dataStream);

                search.Name= dto.Name;
                search.BirthDay= dto.BirthDay;
                search.Email= dto.Email;
                search.Phone= dto.Phone;
                search.Form = dataStream.ToArray();
                search.departmentId= dto.departmentId;

                _context.Update(search);
                _context.SaveChanges();
            }

            return Ok(search);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteStudent(int id)
        {
            var search = await _context.students.FindAsync(id);

            if (search is null)
                return BadRequest("Student is not exist");

            return Ok(search);
        }









    }
}
