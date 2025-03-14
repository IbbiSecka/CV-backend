using CV.Data;
using Microsoft.EntityFrameworkCore;

namespace CV.Repository
{
    public class Resume: IResume
    {
        private readonly DataContext _db;

        public Resume(DataContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Models.ResumeExperience>> GetAll()
        {
            return await _db.ResumeExperiences.ToListAsync();
        }

        // Get all resume experiences as ICollection
        public async Task<ICollection<Models.ResumeExperience>> GetResumeExperiences()
        {
            return await _db.ResumeExperiences.ToListAsync();
        }

        // Get a single experience by ID
        public async Task<Models.ResumeExperience?> GetExperienceById(int id)
        {
            return await _db.ResumeExperiences.FindAsync(id);
        }

        // Add a new experience
        public async Task<Models.ResumeExperience> AddExperience(Models.ResumeExperience experience)
        {
            _db.ResumeExperiences.Add(experience);
            await _db.SaveChangesAsync();
            return experience;
        }

        // Update an existing experience
        public async Task<Models.ResumeExperience?> UpdateExperience(int id, Models.ResumeExperience updatedExperience)
        {
            var existing = await _db.ResumeExperiences.FindAsync(id);
            if (existing == null) return null;

            existing.CompanyName = updatedExperience.CompanyName;
            existing.CompanyLocation = updatedExperience.CompanyLocation;
            existing.Position = updatedExperience.Position;
            existing.Duration = updatedExperience.Duration;
            existing.Description = updatedExperience.Description;

            await _db.SaveChangesAsync();
            return existing;
        }

        // Delete an experience
        public async Task<bool> DeleteExperience(int id)
        {
            var existing = await _db.ResumeExperiences.FindAsync(id);
            if (existing == null) return false;

            _db.ResumeExperiences.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
