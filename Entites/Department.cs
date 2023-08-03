namespace CollegeOfSystem.Entites
{
    public class Department
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Student> Students { get; set; }=new List<Student>();
        public ICollection<Doctor> doctors { get; set; }=new List<Doctor>();
    }
}

