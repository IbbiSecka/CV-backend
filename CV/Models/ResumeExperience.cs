namespace CV.Models
{
    public class ResumeExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public string Position { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public Ibbi Ibbi { get; set; }
    }
}
