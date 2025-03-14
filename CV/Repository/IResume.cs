using CV.Models;

namespace CV.Repository
{
    public interface IResume
    {
        Task<IEnumerable<Models.ResumeExperience>> GetAll();
        Task<ICollection<Models.ResumeExperience>> GetResumeExperiences();
        Task<Models.ResumeExperience?> GetExperienceById(int id);
        Task<Models.ResumeExperience> AddExperience(Models.ResumeExperience experience);
        Task<Models.ResumeExperience?> UpdateExperience(int id, Models.ResumeExperience updatedExperience);
        Task<bool> DeleteExperience(int id);
    }
}
