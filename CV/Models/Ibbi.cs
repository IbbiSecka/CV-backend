namespace CV.Models
{
    public class Ibbi
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string Description { get; set; }
        public string DOB { get; set; }
        public string Img { get; set; }
        public ICollection<Social> Socials { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<ResumeExperience> resumeExperiences { get; set; }
        public ICollection<Language> Languages { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
