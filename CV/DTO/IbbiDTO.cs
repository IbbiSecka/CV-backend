using CV.DTO.NewFolder;

namespace CV.DTO
{
    public class IbbiDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Description { get; set; }
        public string DOB { get; set; }
        public string Img { get; set; }
        public List<SocialDTO> Socials { get; set; }
        public List<ProjectDTO> Projects { get; set; }
        public List<ResumeDto> ResumeExperiences { get; set; }
        public List<LanguageDTO> Languages { get; set; }
        public List<EduDTO> Educations { get; set; }
    }
}
