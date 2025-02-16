using CV.Models;

namespace CV.Repository
{
    public interface IUser
    {
        Task<Ibbi> GetIbbi(); 
    }
}
