using CV.Data;
using Microsoft.EntityFrameworkCore;


namespace CV.Repository
{
    public class Language : ILanguage
    {
            private readonly DataContext _db;

            public Language(DataContext db)
            {
                _db = db;
            }

            public async Task<IEnumerable<Models.Language>> GetAll()
            {
                return await _db.Languages.ToListAsync();
            }

            public async Task<Models.Language> Create(Models.Language language)
            {
            var ibbiExists = await _db.Ibbis.AnyAsync(i => i.Id == language.IbbiId);
            if (!ibbiExists)
            {
                throw new Exception("Invalid IbbiId: No matching Ibbi found.");
            }

            _db.Languages.Add(language);
                await _db.SaveChangesAsync();
                return language;
            }

            public async Task<bool> Delete(int id)
            {
                var existing = await _db.Languages.FindAsync(id);
                if (existing == null) return false;

                _db.Languages.Remove(existing);
                await _db.SaveChangesAsync();
                return true;
            }
    }
}
