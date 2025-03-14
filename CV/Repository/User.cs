using CV.Data;
using CV.Models;
using Microsoft.EntityFrameworkCore;

namespace CV.Repository
{
    public class User : IUser
    {
        private DataContext _db;
        public User(DataContext db)
        {
            _db = db;
        }

        public async Task<Ibbi> GetIbbi()
        {
            return await _db.Ibbis
          .Include(i => i.resumeExperiences)
          .Include(i => i.Projects)
          .Include(i => i.Educations)
          .Include(i => i.Socials)
          .Include(i => i.Languages)
          .FirstOrDefaultAsync(x => x.Id == 1);
        }

        public async Task<Ibbi> UpdateIbbi(int id, Ibbi updated)
        {
            var ibbi =  await _db.Ibbis.FindAsync(id);
            if (ibbi == null) return null;

            ibbi.resumeExperiences = updated.resumeExperiences;
            ibbi.Educations = updated.Educations;
            ibbi.Projects = updated.Projects;
            ibbi.Languages = updated.Languages;
            ibbi.Socials = updated.Socials;

            await _db.SaveChangesAsync();
            return ibbi;


        }
    }
}
