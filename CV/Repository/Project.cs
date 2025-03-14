using CV.Data;
using Microsoft.EntityFrameworkCore;

namespace CV.Repository
{
    public class Project : IProject
    {
            private readonly DataContext _db;

            public Project(DataContext db)
            {
                _db = db;
            }

            public async Task<IEnumerable<Models.Project>> GetAll()
            {
                return await _db.Projects.ToListAsync();
            }

            public async Task<Models.Project> Create(Models.Project project)
            {
                _db.Projects.Add(project);
                await _db.SaveChangesAsync();
                return project;
            }

            public async Task<Models.Project> Update(int id, Models.Project project)
            {
                var existing = await _db.Projects.FindAsync(id);
                if (existing == null) return null;

                existing.Name = project.Name;
                existing.Description = project.Description;
                existing.Img = project.Img;
                existing.Date = project.Date;
                existing.Role = project.Role;

                await _db.SaveChangesAsync();
                return existing;
            }
        
    }
}
