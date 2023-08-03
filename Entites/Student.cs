namespace CollegeOfSystem.Entites
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public Department department { get; set; }
        public int departmentId { get; set; }
        public byte[] Form { get; set; }

    }
}

