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
            return await _db.Ibbis.FirstOrDefaultAsync(x => x.Id == 1);
        }
    }
}
