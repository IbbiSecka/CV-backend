using CV.Data;
using CV.Models;
using Microsoft.EntityFrameworkCore;

namespace CV.Repository
{
    public class Education
    {
        public class EducationRepo : IEducation
        {
            private readonly DataContext _db;

            public EducationRepo(DataContext db)
            {
                _db = db;
            }

            public async Task<IEnumerable<Models.Education>> GetAll()
            {
                return await _db.Educations.ToListAsync();
            }

            public async Task<Models.Education> Create(Models.Education education)
            {
                _db.Educations.Add(education);
                await _db.SaveChangesAsync();
                return education;
            }

            public async Task<Models.Education> Update(int userId, int educationId, Models.Education updatedEducation)
            {
                var existingEducation = await _db.Educations
       .Where(e => e.Id == educationId && e.Ibbi.Id == userId)
       .FirstOrDefaultAsync();

                if (existingEducation == null) return null; // Not found or doesn't belong to the user

                // Update fields
                existingEducation.EducationName = updatedEducation.EducationName;
                existingEducation.Description = updatedEducation.Description;
                existingEducation.EducationSite = updatedEducation.EducationSite;
                existingEducation.Degree = updatedEducation.Degree;

                await _db.SaveChangesAsync();
                return existingEducation;
            }
        }
    }
}
