using CV.Models;

namespace CV.Repository
{
    public interface IProject
    {
        Task<IEnumerable<Models.Project>> GetAll();
        Task<Models.Project> Create(Models.Project project);
        Task<Models.Project> Update(int id, Models.Project project);
        Task<Models.Project> GetOne(int id);
    }
}
