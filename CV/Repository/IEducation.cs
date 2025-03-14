using CV.Models;

namespace CV.Repository
{
    public interface IEducation
    {
        Task<IEnumerable<Models.Education>> GetAll();
        Task<Models.Education> Create(Models.Education education);
        Task<Models.Education> Update(int id, int EducationId, Models.Education updatedEducation);
    }
}
