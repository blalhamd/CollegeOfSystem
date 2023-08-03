using CollegeOfSystem.Entites;

namespace CollegeOfSystem.EntitiesDto
{
    public class StudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public int departmentId { get; set; }
        public IFormFile Form { get; set; }
    }
}
